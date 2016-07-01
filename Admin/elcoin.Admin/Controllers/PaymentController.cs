using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Services.DiscountService;
using bbom.Admin.Core.Services.DiscountService.SaleService;
using bbom.Admin.Core.Services.PaymentService;
using bbom.Admin.Core.Services.PaySystemService;
using bbom.Admin.Core.ViewModels.Pay;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;

namespace elcoin.Admin.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IRepository<PaymentPlan> _paymentPlansRepository;
        private readonly IDiscountService _discountService;
        private readonly IPaySystemService _paySystemService;
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<Payment> _paymentsRepository;
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IRepository<PaymentPlan> paymentPlansRepository, 
            IDiscountService discountService,
            IPaySystemService paySystemService,
            IRepository<AspNetUser> usersRepository, 
            IRepository<Payment> paymentsRepository,
            IPaymentService paymentService)
        {
            _paymentPlansRepository = paymentPlansRepository;
            _discountService = discountService;
            _paySystemService = paySystemService;
            _usersRepository = usersRepository;
            _paymentsRepository = paymentsRepository;
            _paymentService = paymentService;
            _paymentService.OnPayComplited += delegate (int paymentId)
            {
                var payment = _paymentsRepository.GetById(paymentId);
                LogManager.GetCurrentClassLogger().Info("Завершение платежа проставление ролей");
                var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                userManager.AddToRole(payment.UserId, UserRole.User);
                userManager.RemoveFromRole(payment.UserId, UserRole.NotUser);
                userManager.RemoveFromRole(payment.UserId, UserRole.NotPay);
                CoreFasade.UsersHelper.ClearUserCache(payment.AspNetUser.UserName, null);
                HttpContext.Cache.Remove(payment.AspNetUser.UserName + "Payment");
            };
            _paymentService.OnPayComplited += delegate (int paymentId)
            {
                var payment = _paymentsRepository.GetById(paymentId);
                LogManager.GetCurrentClassLogger().Info("Завершение платежа работа со скидками");
                if (payment.Discount != null)
                    SaleMenager.GetSaleProvider(payment.Discount.DiscountType).Execute(payment);
                _discountService.SetUserDiscountOfInvite(payment.UserId);
            };
            _paymentService.OnPayError += delegate (int paymentId)
            {
                var payment = _paymentsRepository.GetById(paymentId);
                LogManager.GetCurrentClassLogger().Info("Ошибка платежа");
                HttpContext.Cache.Remove(payment.AspNetUser.UserName + "Payment");
            };
        }

        // GET: Payment
        /// <summary>
        /// Страница формирования платежа
        /// Выбирается тарифный план и скидка
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var pvm = GetPaymentViewModel();
            return View("StartPay", pvm);
        }


        /// <summary>
        /// Вернет json доступного для тарифного плана скидок
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPaymentDiscountsJson(string paymentId)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var plan = _paymentPlansRepository.GetById(Convert.ToInt32(paymentId));
            var dis = _discountService.GetAllAlowDiscounts(user, new[] {plan});
            var disJson = dis.Select(discount => new DiscountJson
            {
                name = discount.Name,
                id = discount.Id.ToString(),
                amount = discount.DiscountAmount.ToString("###")
            }).ToList();
            return Json(disJson, JsonRequestBehavior.AllowGet);
        }

        // POST: Payment
        /// <summary>
        /// Действие при выборе параметров платежа на странице формирования платежа
        /// Формируется 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> StartPay(PayViewModel model)
        {
            if (model.selectPlanId != 0)
            {
                var payment = await _paymentService.CreatePayment(User.GetUserId(), model.selectDiscountId,
                    model.selectPlanId);
                HttpContext.Cache[User.GetUserName() + "PaymentId"] = payment.Id;
                HttpContext.Cache[payment.Id + "PaymentId"] = payment.Id;
                return RedirectToAction("Pay");
            }
            ModelState.AddModelError("", "Не выбран тарифный план");
            return View(GetPaymentViewModel());
        }

        // GET: Pay
        /// <summary>
        /// Возвращает страницу оплаты, с уже сформированным заказом
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pay()
        {
            var id = (int)HttpContext.Cache[User.GetUserName() + "PaymentId"];
            var payment = _paymentsRepository.GetById(id);
            if (payment == null)
                return View("ConfimPaid");
            return View(payment);
        }

        /// <summary>
        /// Страница тестировани работы метода ожидающего ответ от ЯД
        /// </summary>
        /// <returns></returns>
        [OnlyAdminAuthorize]
        [HttpGet]
        public ActionResult TestPaid(string id)
        {
            var paymentId = id != null ? (int)HttpContext.Cache[id + "PaymentId"] : (int)HttpContext.Cache[User.GetUserName() + "PaymentId"];
            var payment = _paymentsRepository.GetById(paymentId);
            var amount = payment.PaymentPlan.Amount;
            _paymentService.ConfimPayment(amount, paymentId);
            return View("Paid");
        }

        /// <summary>
        /// Страница тестировани работы метода ожидающего ответ от ЯД
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Paid(string id)
        {
            var paymentId = HttpContext.Cache[id + "PaymentService"];
            ViewBag.PaymentId = paymentId;
            return View();
        }

        /// <summary>
        /// Действие при платеже без оплаты(когда сумма равна 0)
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult PayPost(Payment payment)
        {
            decimal payAmount = payment.Amount;
            var id = (int)HttpContext.Cache[User.GetUserName() + "PaymentId"];
            _paymentService.ConfimPayment(payAmount, id);
            return RedirectToAction("ConfimPaid");
        }

        /// <summary>
        /// Действие ожидающие параметры платежа от ЯД
        /// </summary>
        /// <param name="notification_type"></param>
        /// <param name="operation_id"></param>
        /// <param name="label"></param>
        /// <param name="datetime"></param>
        /// <param name="amount"></param>
        /// <param name="withdraw_amount"></param>
        /// <param name="sender"></param>
        /// <param name="sha1_hash"></param>
        /// <param name="currency"></param>
        /// <param name="codepro"></param>
        /// <param name="unaccepted"></param>
        /// <param name="test_notification"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public ActionResult Paid(string notification_type, string operation_id, string label,
            string datetime,
            string amount, string withdraw_amount, string sender, string sha1_hash, string currency, string codepro,
            string unaccepted, string test_notification)
        {
            LogManager.GetCurrentClassLogger().Info("Ответ от Я.Д " +
                                                    "\nnotification_type " + notification_type + "" +
                                                    "\noperation_id " + operation_id +
                                                    "\nlabel " + label +
                                                    "\ndatetime " + datetime +
                                                    "\namount " + amount +
                                                    "\nwithdraw_amount " + withdraw_amount +
                                                    "\nsender " + sender +
                                                    "\nsha1_hash " + sha1_hash +
                                                    "\ncurrency " + currency +
                                                    "\ncodepro " + codepro +
                                                    "\nunaccepted " + unaccepted +
                                                    "\ntest_notification " + test_notification);
            string key = "";
            var ymOptions = new YandexMoneyOptions
            {
                datetime = datetime,
                operation_id = operation_id,
                withdraw_amount = withdraw_amount,
                amount = amount,
                codepro = codepro,
                currency = currency,
                label = label,
                notification_type = notification_type,
                sender = sender,
                sha1_hash = sha1_hash,
                test_notification = test_notification,
                unaccepted = unaccepted
            };
            decimal payAmount = Convert.ToDecimal(withdraw_amount.Replace(".", ","));
            bool hashYm = _paySystemService.ProcessingRespons(ymOptions, key);
            LogManager.GetCurrentClassLogger().Info("Хеш " + hashYm);
            if (hashYm)
            {
                LogManager.GetCurrentClassLogger().Info("найден платеж " + label);
                _paymentService.ConfimPayment(payAmount, Convert.ToInt32(label));
                return new EmptyResult();
            }
            throw new Exception($"Неверный хеш: id {label}");
        }

        /// <summary>
        /// Завершение платежа с выводом информации
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfimPaid(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "Ошибка";
                return View();
            }
            var payment = _paymentsRepository.GetById(id);
            string message = "Ошибка";
            if (payment == null)
            {
                message += " платежа не существует";
                ViewBag.Message = message;
                return View();
            }
            if (payment.Status == 1)
            {
                message = $"Оплата №{payment.Id} системы Соломон прошла успешно!";
                ViewBag.Message = message;
                ViewBag.Update = 0;
                return View();
            }
            if (payment.Status == 0)
            {
                message = $"Оплата №{payment.Id} системы Соломон прошла завершилась с ошибкой!";
                ViewBag.Message = message;
                return View();
            }
            if (payment.Amount < payment.PaymentPlan.Amount)
                message += ". Сумма оплаты не верна.";
            ViewBag.Message = message;
            return View();
        }

        [NonAction]
        private PayViewModel GetPaymentViewModel()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var paymentPlans = CoreFasade.UsersHelper.GetAllAlowPaymentPlans(user);
            var pvm = new PayViewModel
            {
                Discounts = _discountService.GetAllAlowDiscounts(user),
                PaymentPlans = paymentPlans
            };
            return pvm;
        }
    }
}
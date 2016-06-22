using System;
using System.Linq;
using System.Threading.Tasks;
using bbom.Admin.Core.Services.DiscountService.SaleService;
using bbom.Data;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using NLog;

namespace bbom.Admin.Core.Services.PaymentService
{
    public class PaymentService: IPaymentService
    {
        public event EventContainer OnPayComplited;
        public event EventContainer OnPayError;
        
        /// <summary>
        /// Создает новый платеж или возвращает последний незавершенный
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="discountId"></param>
        /// <param name="paymentPlanId"></param>
        /// <returns></returns>
        public async Task<Payment> CreatePayment(string userId, int? discountId, int paymentPlanId)
        {
            var paymentsRepository = DataFasade.GetRepository<Payment>();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Нету данных пользователя");
            }
            var discount = discountId == null ? null : DataFasade.GetRepository<Discount>().GetById(discountId);
            var user = DataFasade.GetRepository<AspNetUser>().GetById(userId);
            var plan = DataFasade.GetRepository<PaymentPlan>().GetById(paymentPlanId);
            //найти старые незавершенные платежи
            var date24 = DateTime.Now.AddHours(-24);
            var paymentOld =
                paymentsRepository
                    .GetAll(
                    ).FirstOrDefault(payment1 => payment1.Status == PaymentStatus.Created
                        && payment1.Date < DateTime.Now && payment1.Date > date24
                        && payment1.UserId == userId);
            if (paymentOld != null)
            {
                //применить план
                paymentOld.PaymentPlanId = plan.Id;
                paymentOld.PaymentPlan = plan;
                paymentOld.Amount = plan.Amount;
                if (paymentOld.AspNetUser == null)
                {
                    paymentOld.AspNetUser = user;
                }
                if (discount != null)
                {
                    //применить скидку
                    paymentOld.Amount = SaleMenager.GetSaleProvider(discount.DiscountType).Sale(plan, discount, userId);
                    paymentOld.DiscountId = discountId;
                }
                paymentsRepository.SaveChanges();
                return paymentOld;
            }
            //если их нету создать новый
            var payment = new Payment
            {
                UserId = userId,
                Date = DateTime.Now,
                Status = 0,
                Amount =
                    discount == null
                        ? plan.Amount
                        : SaleMenager.GetSaleProvider(discount.DiscountType).Sale(plan, discount, userId),
                PaymentPlanId = paymentPlanId,
                DiscountId = discountId,
                AspNetUser = user
            };
            if (plan.WorkAmount != null)
                payment.EndDate = DateTime.Now.AddMonths(plan.WorkAmount.Value);
            await paymentsRepository.InsertAsync(payment);
            return payment;
        }

        /// <summary>
        /// Переводит платеж в  статус "Оплачено"
        /// </summary>
        /// <param name="payAmount"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public void ConfimPayment(decimal payAmount, int paymentId)
        {
            var paymentsRepository = DataFasade.GetRepository<Payment>();
            var payment = paymentsRepository.GetById(paymentId);
            LogManager.GetCurrentClassLogger().Info("payment " + paymentId);
            LogManager.GetCurrentClassLogger().Info("payment " + payAmount);
            LogManager.GetCurrentClassLogger().Info("юзер " + payment.UserId);
            //обязательно проверить сумму платежа
            try
            {
                if (payment.Amount <= payAmount)
                {
                    if (payment.Status == PaymentStatus.Completed)
                    {
                        LogManager.GetCurrentClassLogger().Info("Платеж уже завершен");
                        return;
                    }
                    payment.Status = PaymentStatus.Completed;
                    LogManager.GetCurrentClassLogger().Info("Статус обновлен на завершен");
                    //удалить все незавершенные платежи этого пользователя
                    //var oldPayments =
                    //    new List<Payment>(
                    //        payment.AspNetUser.Payments.Where(payment1 => payment1.Status == PaymentStatus.Created));
                    //foreach (var oldPayment in oldPayments)
                    //{
                    //    if (oldPayment.Id != _payment.Id)
                    //        _paymentsRepository.Delete(oldPayment);
                    //}
                    paymentsRepository.Edit(payment);
                    OnPayComplited?.Invoke(payment.Id); //событие заерщения платежа
                    LogManager.GetCurrentClassLogger().Info("payment " + payment.Id + " ---------- Завершен");
                }
            }
            catch (Exception e)
            {
                OnPayError?.Invoke(payment.Id);
                throw e;
            }
        }
    }
}
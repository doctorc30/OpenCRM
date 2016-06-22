using System;
using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.Services.DiscountService.SaleService;
using bbom.Data;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService
{
    public class DiscountService: IDiscountService
    {
        private readonly IAccessService _accessService;

        public DiscountService(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public void SaleExecute(Payment payment)
        {
            SaleMenager.GetSaleProvider(payment.Discount.DiscountType).Execute(payment);
        }

        /// <summary>
        /// Возвращает скидки доступные для всех тарифных планов
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<Discount> GetAllAlowDiscounts(AspNetUser user)
        {
            List<Discount> discounts = new List<Discount>();
            //доступные типы скидок
            var roleDTs = _accessService.GetUserAlowDiscountTypes(user);
            foreach (var dt in roleDTs)
            {
                if (dt.PaymentPlans.Count == 0)
                {
                    discounts.AddRange(dt.Discounts); //скидки по роли
                }
            }
            foreach (var receiveDiscount in user.ReceiveDiscounts)
            {
                if (receiveDiscount.DiscountType.PaymentPlans.Count == 0)
                {
                    discounts.Add(receiveDiscount); //полученные скидки
                }
            }
            var invDis = user.UserInvitedDiscounts.FirstOrDefault(); //скидка за приглащения 
            if (invDis?.Discount.DiscountType.PaymentPlans.Count == 0
                && Math.Truncate(Convert.ToDecimal(invDis.Amount / GlobalConstants.InviteDiscountAmount)) > 0)
            {
                discounts.Add(invDis.Discount);
            }
            return discounts;
        }

        /// <summary>
        /// Возвращает скидля доступные только для определенных тарифных планов
        /// </summary>
        /// <param name="user"></param>
        /// <param name="paymentPlans"></param>
        /// <returns></returns>
        public ICollection<Discount> GetAllAlowDiscounts(AspNetUser user, ICollection<PaymentPlan> paymentPlans)
        {
            List<Discount> discounts = new List<Discount>();
            var roleDTs = _accessService.GetUserAlowDiscountTypes(user);
            foreach (var plan in paymentPlans)
            {
                foreach (var dt in roleDTs)
                {
                    if (dt.PaymentPlans.Contains(plan))
                    {
                        discounts.AddRange(dt.Discounts); //скидки по роли
                    }
                }
                foreach (var receiveDiscount in user.ReceiveDiscounts)
                {
                    if (receiveDiscount.DiscountType.PaymentPlans.Contains(plan))
                    {
                        discounts.Add(receiveDiscount); //полученные скидки
                    }
                }
                var invDis = user.UserInvitedDiscounts.FirstOrDefault(); //скидка за приглащения 
                if (invDis != null
                    &&
                    (invDis.Discount.DiscountType.PaymentPlans.Contains(plan) &&
                     invDis.Amount%GlobalConstants.InviteDiscountAmount > 0))
                {
                    discounts.Add(invDis.Discount);
                }
            }
            return discounts;
        }

        public void SetUserDiscountOfInvite(string userId)
        {
            var usersRepo = DataFasade.GetRepository<AspNetUser>();
            var discountTypesRepository = DataFasade.GetRepository<DiscountType>();
            var user = usersRepo.GetById(userId).InvitedAspNetUser;
            var dis =
                discountTypesRepository.GetById(Data.ModelPartials.Constants.DiscountType.InviteDiscountType)
                    .Discounts.FirstOrDefault();
            if (dis == null)
                throw new Exception("Отсутствует скидка за приглашение");
            var invDis = user.UserInvitedDiscounts.FirstOrDefault();
            if (invDis == null)
                user.UserInvitedDiscounts.Add(new UserInvitedDiscount
                {
                    AspNetUser = user,
                    Discount = dis,
                    Amount = 1
                });
            else
            {
                invDis.Amount++;
            }
            usersRepo.SaveChanges();
        }
    }
}
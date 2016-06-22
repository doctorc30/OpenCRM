using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using bbom.Data;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService.SaleService
{
    public class InviteSaleProvider : DefaulSaleProvider
    {
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override decimal Sale(PaymentPlan plan, Discount discount, string userId)
        {
            var usersRepository = DataFasade.GetRepository<AspNetUser>();
            var user = usersRepository.GetById(userId);
            if (user == null)
                throw new Exception("Для осуществления скидки не найден пользователь");
            var invDis = user.UserInvitedDiscounts.FirstOrDefault();
            if (invDis == null)
                throw new Exception("Для осуществления скидки не найдена скидка");
            if (Math.Truncate(Convert.ToDecimal(invDis.Amount / GlobalConstants.InviteDiscountAmount)) == 0)
                throw new Exception("Недостаточное колличество приглащенных пользователей");
            //invDis.Amount = invDis.Amount - 5;
            //todo сделать подпись на собите завершение платяжа
            return base.Sale(plan, discount, userId);
        }

        public override void Execute(Payment payment)
        {
            var invDis = payment.AspNetUser.UserInvitedDiscounts.SingleOrDefault();
            if (invDis == null)
                throw new Exception("Для осуществления скидки не найдена скидка");
            invDis.Amount = invDis.Amount - 5;
            DataFasade.GetRepository<AspNetUser>().SaveChanges();
        }
    }
}
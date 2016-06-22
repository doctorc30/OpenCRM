using System.Linq;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Data;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class ClaimsHelper : IClaimsHelper
    {
        public void SetSingleClaim(AspNetUser user, Claim claim)
        {
            var claimTemplate = user.AspNetUserClaims.FirstOrDefault(c => c.ClaimType == claim.ValueType);
            if (claimTemplate != null)
            {
                claimTemplate.ClaimValue = claim.Value;
                
            }
            else
            {
                user.AspNetUserClaims.Add(new AspNetUserClaim
                {
                    ClaimType = claim.ValueType,
                    ClaimValue = claim.Value
                });
            }
            DataFasade.GetRepository<AspNetUser>().SaveChanges();
        }

        public Claim GetSingleClaim(AspNetUser user, string name)
        {
            var claim = new Claim {ValueType = name, Value = ""};
            var claimTemplate = user.AspNetUserClaims.FirstOrDefault(c => c.ClaimType == name);
            if (claimTemplate != null)
            {
                claim.Value = claimTemplate.ClaimValue;
                return claim;
            }
            return claim;
        }
    }
}
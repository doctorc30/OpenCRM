using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface IClaimsHelper
    {
        void SetSingleClaim(AspNetUser user, Claim claim);
        Claim GetSingleClaim(AspNetUser user, string name);
    }
}
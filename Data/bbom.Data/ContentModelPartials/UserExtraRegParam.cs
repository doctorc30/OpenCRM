using bbom.Data.IdentityModel;

namespace bbom.Data.ContentModel
{
    public partial class UserExtraRegParam
    {
        public AspNetUser AspNetUser => DataFasade.GetRepository<AspNetUser>().GetById(UserId);
    }
}
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.TreeCreator
{
    public interface ITreeUsers
    {
        object GetTreeUsersJson(AspNetUser user);
        object GetTreeUsersWithLineJson(AspNetUser user, int line);
    }
}
using System.Collections.Generic;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface IUserRolesCache
    {
        ICollection<string> Get();
    }
}
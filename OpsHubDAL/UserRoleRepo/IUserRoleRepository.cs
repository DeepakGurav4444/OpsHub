using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRoleRepo
{
    public interface IUserRoleRepository
    {
        Task<UserRoles> GetAdminRoleByModTenId(int TenantId,int ModuleId);
    }
}

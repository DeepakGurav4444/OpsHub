using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRolePerRepo
{
    public interface IUserRolePerRepository
    {
        Task<UserRolePermissions> GetUserRolePerByModPerId(int ModuleId, int PermissionId);
        Task<List<string>> GetUserPermissionsByUserId(int UserId);
    }
}

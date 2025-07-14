using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRoleMapRepo
{
    public interface IUserRolMapRepository
    {
        Task<bool> IsMappedRole(int RoleId);
        Task<int> MapUserRole(List<UserRoleMap> userRoleMap);
        Task<UserRoleMap> GetUserRole(int UserId);
    }
}

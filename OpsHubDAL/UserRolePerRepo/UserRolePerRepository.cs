using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRolePerRepo
{
    public class UserRolePerRepository : IUserRolePerRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;

        public UserRolePerRepository(ILoggerManager loggerManager,IOpsHubContext opsHubContext) 
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }

        public async Task<List<string>> GetUserPermissionsByUserId(int UserId)
        {
            _loggerManager.LogInfo("Entry UserRolePerRepository => GetUserPermissionsByRoleId");
            var dataReult = await
                (from urm in _opsHubContext.UserRoleMap
                 join rp in _opsHubContext.UserRolePermissions on urm.RoleId equals rp.RoleId
                 join pm in _opsHubContext.UserPermissions on rp.PermissionId equals pm.IduserPermissions
                 where urm.TenUserId == UserId
                 select pm.PermissionName).ToListAsync();
            _loggerManager.LogInfo("Exit UserRolePerRepository => GetUserPermissionsByRoleId");
            return dataReult;
        }

        public async Task<UserRolePermissions> GetUserRolePerByModPerId(int ModuleId, int PermissionId)
        {
            _loggerManager.LogInfo("Entry UserRolePerRepository => GetUserRolePerByModPerId");
            var dataReult = await _opsHubContext.UserRolePermissions.FirstOrDefaultAsync(x => x.ModuleId == ModuleId && x.PermissionId == PermissionId && x.IsActive==1);
            _loggerManager.LogInfo("Exit UserRolePerRepository => GetUserRolePerByModPerId");
            return dataReult;
        }
    }
}

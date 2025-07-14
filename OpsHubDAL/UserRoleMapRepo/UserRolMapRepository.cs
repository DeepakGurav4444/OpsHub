using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRoleMapRepo
{
    public class UserRolMapRepository : IUserRolMapRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public UserRolMapRepository(ILoggerManager loggerManager, IOpsHubContext opsHubContext)
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }

        public async Task<UserRoleMap> GetUserRole(int UserId)
        {
            _loggerManager.LogInfo("Entry UserRolePerRepository => GetUserRole");
            var dataReult = await _opsHubContext.UserRoleMap.FirstOrDefaultAsync(x => x.TenUserId == UserId && x.IsActive==1);
            _loggerManager.LogInfo("Exit UserRolePerRepository => GetUserRole");
            return dataReult;
        }

        public async Task<bool> IsMappedRole(int RoleId)
        {
            _loggerManager.LogInfo("Entry UserRolePerRepository => GetUserRolePerByModPerId");
            var dataReult = await _opsHubContext.UserRoleMap.AnyAsync(x => x.RoleId==RoleId && x.IsActive == 1);
            _loggerManager.LogInfo("Exit UserRolePerRepository => GetUserRolePerByModPerId");
            return dataReult;
        }

        public async Task<int> MapUserRole(List<UserRoleMap> userRoleMap)
        {
            _loggerManager.LogInfo("Entry UserRolePerRepository => MapUserRole");
            await _opsHubContext.UserRoleMap.AddRangeAsync(userRoleMap);
            var dataReult =await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit UserRolePerRepository => MapUserRole");
            return dataReult;
        }
    }
}

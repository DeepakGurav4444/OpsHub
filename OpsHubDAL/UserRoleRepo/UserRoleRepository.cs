using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.UserRoleRepo
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IOpsHubContext _opsHubContext;
        public UserRoleRepository(ILoggerManager loggerManager,IOpsHubContext opsHubContext) 
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }
        public async Task<UserRoles> GetAdminRoleByModTenId(int TenantId, int ModuleId)
        {
            _loggerManager.LogInfo("Entry UserRoleRepository => GetAdminRoleByModTenId");
            _loggerManager.LogInfo("Exit UserRoleRepository => GetAdminRoleByModTenId");
            return await _opsHubContext.UserRoles.FirstOrDefaultAsync(x => x.TenantId == TenantId && x.ModuleId == ModuleId && x.IsForAdmin==1);
        }
    }
}

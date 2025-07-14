using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenUserRepo
{
    public class TenantUserRepository : ITenantUserRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public TenantUserRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _opsHubContext = opsHubContext;
            _loggerManager = loggerManager;
        }

        public async Task<TenUsers> AddTenUser(TenUsers tenUser)
        {
            _loggerManager.LogInfo("Entry TenantUserRepository => AddTenUser");
            var dataReult = await _opsHubContext.TenUsers.AddAsync(tenUser);
            await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit TenantUserRepository => AddTenUser");
            return dataReult.Entity;
        }

        public async Task<TenUsers> GetTenUserById(int UserId)
        {
            _loggerManager.LogInfo("Entry TenantUserRepository => GetTenUserById");
            var dataReult = await _opsHubContext.TenUsers.FirstOrDefaultAsync(x => x.IdtenUsers == UserId);
            _loggerManager.LogInfo("Exit TenantUserRepository => GetTenUserById");
            return dataReult;
        }

        public async Task<TenUsers> GetUserByEmailPass(string EmailId, string Password)
        {
            _loggerManager.LogInfo("Entry TenantUserRepository => GetUserByEmailPass");
            var dataReult = await _opsHubContext.TenUsers.FirstOrDefaultAsync(x => x.EmailId == EmailId && x.TenUserPass == Password && x.IsActive == 1);
            _loggerManager.LogInfo("Exit TenantUserRepository => GetUserByEmailPass");
            return dataReult;
        }

        public async Task<bool> IsAdminExist(int ModuleId, int TenantId)
        {
            _loggerManager.LogInfo("Entry TenantUserRepository => IsAdminExist");
            var dataReult = await _opsHubContext.TenUsers.AnyAsync(x=>x.ModulesId == ModuleId && x.TenantId == TenantId && x.IsAdmin==1);
            _loggerManager.LogInfo("Exit TenantUserRepository => IsAdminExist");
            return dataReult;
        }
    }
}

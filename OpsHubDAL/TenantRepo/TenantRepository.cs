using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenantRepo
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IOpsHubContext _opsHubContext;
        public TenantRepository(ILoggerManager loggerManager,IOpsHubContext opsHubContext) 
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }

        public async Task<TenTenants> AddTenant(TenTenants tenTenants)
        {
            _loggerManager.LogInfo("Entry TenantRepository => AddTenant");
            var dataResult = await _opsHubContext.TenTenants.AddAsync(tenTenants);
            await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit TenantRepository => AddTenant");
            return dataResult.Entity;
        }

        public async Task<TenTenants> GetTenantByEmailPass(string EmailId, string Password)
        {
            _loggerManager.LogInfo("Entry TenantRepository => GetTenantByEmailPass");
            var dataResult = await _opsHubContext.TenTenants.FirstOrDefaultAsync(x=>x.EmailId == EmailId && x.TenPass == Password && x.IsActive==1);
            await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit TenantRepository => GetTenantByEmailPass");
            return dataResult;
        }
    }
}

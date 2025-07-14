using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenModRepo
{
    public class TenantModuleRepository : ITenantModuleRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public TenantModuleRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }
        public async Task<int> AddTenantModules(List<TenModules> tenModules)
        {
            _loggerManager.LogInfo("Entry TenantModuleRepository => AddTenantModules");
            await _opsHubContext.TenModules.AddRangeAsync(tenModules);
            var dataResult = await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit TenantModuleRepository => AddTenantModules");
            return dataResult;
        }

        public async Task<List<TenModules>> GetTenantModulesById(int tenantId)
        {
            _loggerManager.LogInfo("Entry TenantModuleRepository => GetTenantModulesById");
            var dataResult = await _opsHubContext.TenModules.Where(x => x.TenantId == tenantId).ToListAsync();
            _loggerManager.LogInfo("Exit TenantModuleRepository => GetTenantModulesById");
            return dataResult;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenSubMapRepo
{
    public class TenantSubMapRepository : ITenantSubMapRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public TenantSubMapRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _opsHubContext = opsHubContext;
            _loggerManager = loggerManager;
        }

        public async Task<TenSubMap> AddTenSubPlan(TenSubMap tenSubMap)
        {
            _loggerManager.LogInfo("Entry TenantModuleRepository => AddTenSubPlan");
            var dataResult = await _opsHubContext.TenSubMap.AddAsync(tenSubMap);
            await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit TenantModuleRepository => AddTenSubPlan");
            return dataResult.Entity;
        }

        public async Task<List<TenSubMap>> GetTenSubMap(int tenantId)
        {
            _loggerManager.LogInfo("Entry TenantModuleRepository => GetTenSubMap");
            var dataResult = await _opsHubContext.TenSubMap.Where(x => x.TenantId == tenantId && x.IsActive==1).ToListAsync();
            _loggerManager.LogInfo("Exit TenantModuleRepository => GetTenSubMap");
            return dataResult;
        }
    }
}

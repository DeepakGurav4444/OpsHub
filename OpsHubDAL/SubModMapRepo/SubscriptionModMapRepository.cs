using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SubModMapRepo
{
    public class SubscriptionModMapRepository : ISubscriptionModMapRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public async Task<List<SubModMap>> GetMappedModulesBySubId(int SubPlanId)
        {
            _loggerManager.LogInfo("Entry SubscriptionModMapRepository => GetMappedModulesBySubId");
            var dataReult = await _opsHubContext.SubModMap.Where(x => x.SubPlanId == SubPlanId && x.IsActive == 1).ToListAsync();
            _loggerManager.LogInfo("Exit SubscriptionModMapRepository => GetMappedModulesBySubId");
            return dataReult;
        }
    }
}

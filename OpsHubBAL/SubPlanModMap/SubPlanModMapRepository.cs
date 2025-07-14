using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.SubPlanModMap
{
    public class SubPlanModMapRepository : ISubPlanModMapRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public SubPlanModMapRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _opsHubContext = opsHubContext;
            _loggerManager = loggerManager;
        }

        public async Task<List<SubModMap>> GetSubPlanModListBySubId(List<int> SubPlanIdList)
        {
            _loggerManager.LogInfo("Entry SubPlanModMapRepository => GetSubPlanModListBySubId");
            var dataResult = await _opsHubContext.SubModMap.Where(x => SubPlanIdList.Contains(x.SubPlanId)).ToListAsync();
            _loggerManager.LogInfo("Exit SubPlanModMapRepository => GetSubPlanModListBySubId");
            return dataResult;
        }

        public async Task<List<SubModMap>> GetSubPlanModMapBySubId(int SubPlanId)
        {
            _loggerManager.LogInfo("Entry SubPlanModMapRepository => GetSubPlanModMapBySubId");
            var dataResult = await _opsHubContext.SubModMap.Where(x => x.SubPlanId == SubPlanId).ToListAsync();
            _loggerManager.LogInfo("Exit SubPlanModMapRepository => GetSubPlanModMapBySubId");
            return dataResult;
        }
    }
}

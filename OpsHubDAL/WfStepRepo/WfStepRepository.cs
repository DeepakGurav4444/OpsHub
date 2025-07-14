using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.WfStepRepo
{
    public class WfStepRepository : IWfStepRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;

        public WfStepRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        {
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }
        public async Task<WfSteps> GetWfStepByUserId(int UserId)
        {
            _loggerManager.LogInfo("Entry WfStepRepository => GetWfStepByRoleId");
            _loggerManager.LogInfo("Exit WfStepRepository => GetWfStepByRoleId");
            return await 
                (from urm in _opsHubContext.UserRoleMap
                 join wfs in _opsHubContext.WfSteps on urm.RoleId equals wfs.RoleId
                 where urm.TenUserId == UserId
                 select wfs
                 ).FirstOrDefaultAsync();
        }

        public async Task<WfSteps> GetWfStepByWfIdOrder(int wfId, int stepOrder)
        {
            _loggerManager.LogInfo("Entry WfStepRepository => GetWfStepByWfIdOrder");
            _loggerManager.LogInfo("Exit WfStepRepository => GetWfStepByWfIdOrder");
            return await _opsHubContext.WfSteps.FirstOrDefaultAsync(x=>x.WorkFlowId==wfId && x.StepOrder==stepOrder);
        }
    }
}

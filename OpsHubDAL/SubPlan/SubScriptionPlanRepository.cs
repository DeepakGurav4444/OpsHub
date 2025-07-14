using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using OpsHubDTOModel.SubPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SubPlan
{
    public class SubScriptionPlanRepository : ISubScriptionPlanRepository
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IOpsHubContext _opsHubContext;
        public SubScriptionPlanRepository(IOpsHubContext opsHubContext,ILoggerManager loggerManager) 
        { 
            _loggerManager = loggerManager;
            _opsHubContext = opsHubContext;
        }

        public async Task<SubPlans> GetSubPlanById(int SubPlanId)
        {
            _loggerManager.LogInfo("Entry SubScriptionPlanRepository => GetSubPlanById");
            var dataResult = await _opsHubContext.SubPlans.FirstOrDefaultAsync(sp=>sp.IdsubPlans == SubPlanId);
            _loggerManager.LogInfo("Exit SubScriptionPlanRepository => GetSubPlanById");
            return dataResult;
        }

        public async Task<List<GetSubPlansResponse_DTO>> GetSubPlans()
        {
            _loggerManager.LogInfo("Entry SubScriptionPlanRepository => GetSubPlans");
            var dataResult = await _opsHubContext.SubPlans.Where(sp => sp.IsActive == 1)
                .Select(a => new GetSubPlansResponse_DTO() {
                    IdsubPlans = a.IdsubPlans,
                    PlanName = a.PlanName,
                    MaxUsers = a.MaxUsers,
                    Price = a.Price,
                    DurationInMonth = a.DurationInMonth,
                    Modules = a.SubModMap.Where(sm=> sm.IsActive==1).Select(m => m.Module).Where(ms=>ms.IsActive==1).Select(b=>new Modules() 
                    {
                        Description = b.Description,
                        IconUrl = b.IconUrl,
                        IdmodModules = b.IdmodModules,
                        ModuleName = b.ModuleName
                    }).ToList()
            }).ToListAsync();
            _loggerManager.LogInfo("Exit SubScriptionPlanRepository => GetSubPlans");
            return dataResult;
        }
    }
}

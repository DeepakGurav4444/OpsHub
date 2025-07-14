using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDAL.SubPlan;
using OpsHubDTOModel.SubPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.SubPlan
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ISubScriptionPlanRepository _subScriptionPlanRepository;
        public SubscriptionPlanService(ILoggerManager loggerManager,ISubScriptionPlanRepository subScriptionPlanRepository) 
        {
            _loggerManager = loggerManager;
            _subScriptionPlanRepository = subScriptionPlanRepository;
        }
        public async Task<ResultWithDataDTO<List<GetSubPlansResponse_DTO>>> GetSubPlan()
        {
            _loggerManager.LogInfo("Entry SubscriptionPlanService => GetSubPlan");
            ResultWithDataDTO<List<GetSubPlansResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetSubPlansResponse_DTO>>
            {
                IsSuccessful = false
            };

            var dataResult = await _subScriptionPlanRepository.GetSubPlans();

            if (dataResult.Any())
            {
                resultWithDataBO.Data = dataResult;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plans fetched successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plans are not present.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit SubscriptionPlanService => GetSubPlan");
            return resultWithDataBO;
        }
    }
}

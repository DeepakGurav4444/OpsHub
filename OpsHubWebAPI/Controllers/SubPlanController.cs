using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsHubBAL.SubPlan;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.SubPlan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubPlanController : Controller
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        public SubPlanController(ILoggerManager loggerManager,ISubscriptionPlanService subscriptionPlanService) 
        {
            _loggerManager = loggerManager;
            _subscriptionPlanService = subscriptionPlanService;
        }

        [Authorize(Policy = "subscribe_plan")]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSubsPlans()
        {
            ResultWithDataDTO<List<GetSubPlansResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetSubPlansResponse_DTO>> { IsSuccessful = false };
            var userId = User.FindFirst("UserId")?.Value;
            _loggerManager.LogInfo("Entry SubPlanController => GetSubsPlans");

            resultWithDataDTO = await _subscriptionPlanService.GetSubPlan();

            _loggerManager.LogInfo("Exit AdminController => GetSubsPlans");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}

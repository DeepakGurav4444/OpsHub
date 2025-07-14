using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsHubBAL.Tenant;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Tenant;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ITenantService _tenantService;
        public TenantController(ILoggerManager loggerManager,ITenantService tenantService) 
        {
            _loggerManager = loggerManager;
            _tenantService = tenantService;
        }

        [Authorize(Policy = "create_tenant")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTenant([FromBody] AddTenantsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Tenant Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.SysAdminId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry TenantController => AddTenant");

            resultWithDataDTO = await _tenantService.RegisterTenant(request_DTO);

            _loggerManager.LogInfo("Exit TenantController => AddTenant");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "subscribe_plan")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SubscribeToPlan([FromBody] SubscribePlanRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Plan Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.TenantId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry TenantController => SubscribeToPlan");

            resultWithDataDTO = await _tenantService.SubscribeToPlan(request_DTO);

            _loggerManager.LogInfo("Exit TenantController => SubscribeToPlan");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}

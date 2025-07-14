using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsHubBAL.TenantUser;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Tenant;
using OpsHubDTOModel.TenantUser;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenantUserController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ITenantUserService _tenantUserService;
        public TenantUserController(ILoggerManager loggerManager,ITenantUserService tenantUserService) 
        {
            _loggerManager = loggerManager;
            _tenantUserService = tenantUserService;
        }

        [Authorize(Policy = "create_admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTenantSuperAdmin([FromBody] AddTenantSuperAdminRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Tenant User Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.TenantId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry TenantController => AddTenantSuperAdmin");
            resultWithDataDTO = await _tenantUserService.AddTenantSuperAdmin(request_DTO);
            _loggerManager.LogInfo("Exit TenantController => AddTenantSuperAdmin");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "create_user")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTenantUser([FromBody] AddTenantUserRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Tenant User Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.PrincipalId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry TenantController => AddTenantUser");
            resultWithDataDTO = await _tenantUserService.AddTenantUser(request_DTO);
            _loggerManager.LogInfo("Exit TenantController => AddTenantUser");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        //[Authorize(Policy = "create_user")]
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> AsignRoleToUser([FromBody] AsignRoleToUserRequest_DTO request_DTO)
        //{
        //    ResultWithDataDTO<int> resultWithDataDTO =
        //        new ResultWithDataDTO<int> { IsSuccessful = false };

        //    if (request_DTO == null)
        //    {
        //        resultWithDataDTO.IsBusinessError = true;
        //        resultWithDataDTO.BusinessErrorMessage = "Error,Tenant User Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
        //        return BadRequest(resultWithDataDTO);
        //    }
        //    _loggerManager.LogInfo("Entry TenantController => AsignRoleToUser");
        //    resultWithDataDTO = await _tenantUserService.AsignRoleToUser(request_DTO);
        //    _loggerManager.LogInfo("Exit TenantController => AsignRoleToUser");
        //    if (resultWithDataDTO.IsSuccessful)
        //    { return Ok(resultWithDataDTO); }
        //    else { return BadRequest(resultWithDataDTO); }
        //}
    }
}

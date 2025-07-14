using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsHubBAL.Login;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Login;
using OpsHubDTOModel.Tenant;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ILoginService _loginService;
        public LoginController(ILoggerManager loggerManager,ILoginService loginService) 
        {
            _loggerManager = loggerManager;
            _loginService = loginService;
        }

        [Authorize(AuthenticationSchemes = "CustomTokenAuthenticationScheme")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginUser([FromBody] LoginAdminRequest_DTO request_DTO)
        {
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<LoginResponse_DTO> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Login Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry LoginController => LoginUser");

            resultWithDataDTO = await _loginService.LoginUser(request_DTO);

            _loggerManager.LogInfo("Exit LoginController => LoginUser");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(AuthenticationSchemes = "CustomTokenAuthenticationScheme")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginTenant([FromBody] LoginAdminRequest_DTO request_DTO)
        {
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<LoginResponse_DTO> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Login Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry LoginController => LoginTenant");

            resultWithDataDTO = await _loginService.LoginTenant(request_DTO);

            _loggerManager.LogInfo("Exit LoginController => LoginTenant");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(AuthenticationSchemes = "CustomTokenAuthenticationScheme")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginSysAdmin([FromBody] LoginAdminRequest_DTO request_DTO)
        {
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<LoginResponse_DTO> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Login Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry LoginController => LoginSysAdmin");

            resultWithDataDTO = await _loginService.LoginSysAdmin(request_DTO);

            _loggerManager.LogInfo("Exit LoginController => LoginSysAdmin");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(AuthenticationSchemes = "CustomTokenAuthenticationScheme")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest_DTO request_DTO)
        {
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<LoginResponse_DTO> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Token Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry LoginController => RefreshToken");

            resultWithDataDTO = await _loginService.RefreshToken(request_DTO);

            _loggerManager.LogInfo("Exit LoginController => RefreshToken");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}

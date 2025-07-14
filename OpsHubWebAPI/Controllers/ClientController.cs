using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsHubBAL.Client;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Client;
using OpsHubDTOModel.Tenant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsHubWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ILoggerManager _loggerManager;
        private IClientService _clientService;
        public ClientController(ILoggerManager loggerManager, IClientService clientService) 
        {
            _loggerManager = loggerManager;
            _clientService = clientService;
        }

        [Authorize(AuthenticationSchemes = "CustomTokenAuthenticationScheme")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterClient([FromBody] AddClientRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry ClientController => RegisterClient");

            resultWithDataDTO = await _clientService.RegisterClient(request_DTO);

            _loggerManager.LogInfo("Exit ClientController => RegisterClient");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "create_entry")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateClient([FromBody] AddClientRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.UserId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ClientController => CreateClient");

            resultWithDataDTO = await _clientService.RegisterClient(request_DTO);

            _loggerManager.LogInfo("Exit ClientController => CreateClient");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


        [Authorize(Policy = "create_tenant")]
        [HttpGet]
        [Route("[action]/{ClientId}")]
        public async Task<IActionResult> GetClientInfo(int ClientId)
        {
            ResultWithDataDTO<GetClientInfoResponse_DTO> resultWithDataDTO =
                new ResultWithDataDTO<GetClientInfoResponse_DTO> { IsSuccessful = false };

            if (ClientId == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry ClientController => GetClientInfo");

            resultWithDataDTO = await _clientService.GetClientInfo(ClientId);

            _loggerManager.LogInfo("Exit ClientController => GetClientInfo");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "approve_entry")]
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ApproveClient([FromBody] ApproveClientRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.UserId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ClientController => ApproveClient");

            resultWithDataDTO = await _clientService.ApproveClient(request_DTO);

            _loggerManager.LogInfo("Exit ClientController => ApproveClient");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "create_tenant")]
        [HttpGet]
        [Route("[action]/{userId}/{isActive}")]
        public async Task<IActionResult> GetClientsByUserId(int userId, int isActive)
        {
            ResultWithDataDTO<List<GetClientInfoResponse_DTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<GetClientInfoResponse_DTO>> { IsSuccessful = false };

            if (userId == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userIdData = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdData) || !int.TryParse(userIdData, out int parsedId) || (parsedId != userId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ClientController => GetClientsByUserId");

            resultWithDataDTO = await _clientService.GetClientsByUserId(userId,isActive);

            _loggerManager.LogInfo("Exit ClientController => GetClientsByUserId");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize(Policy = "final_approve")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendNotificationToClient([FromBody] SendClientNotificationRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };

            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Client Data Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedId) || (parsedId != request_DTO.UserId))
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.Message = "Invalid or mismatched user identity.";
                return Unauthorized(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry ClientController => SendNotificationToClient");

            resultWithDataDTO = await _clientService.SendNotificationToClient(request_DTO);

            _loggerManager.LogInfo("Exit ClientController => SendNotificationToClient");

            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDAL.ClientRepo;
using OpsHubDAL.ClientWfRepo;
using OpsHubDAL.DataModel;
using OpsHubDAL.NotificationRepo;
using OpsHubDAL.WfStepRepo;
using OpsHubDTOModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Client
{
    public class ClientService : IClientService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IClientRepository _clientRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IClientWfRepository _clientWfRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWfStepRepository _wfStepRepository;
        private readonly IOpsHubContext _opsHubContext;
        private readonly IMapper _mapper;
        public ClientService(ILoggerManager loggerManager, IClientRepository clientRepository, INotificationRepository notificationRepository, IClientWfRepository clientWfRepository, IWfStepRepository wfStepRepository, IOpsHubContext opsHubContext, IHttpContextAccessor httpContextAccessor, IMapper mapper) 
        {
            _loggerManager = loggerManager;
            _clientRepository = clientRepository;
            _notificationRepository = notificationRepository;
            _clientWfRepository = clientWfRepository;
            _wfStepRepository = wfStepRepository;
            _opsHubContext = opsHubContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ResultWithDataDTO<int>> ApproveClient(ApproveClientRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ClientService => ApproveClient");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            var existData = await _clientRepository.GetClientById(request_DTO.ClientId);
            if (existData != null)
            {
                var validationResult = await ValidateStep(request_DTO.ClientId);
                if (validationResult.Item1)
                {
                    existData.IsActive = 1;
                    var updateResult = await _clientRepository.UpdateClient(existData);
                    if (updateResult != null)
                    {
                        var stepData = await _wfStepRepository.GetWfStepByUserId(validationResult.Item2);
                        var newStep = new ClientWfSteps()
                        {
                            ClientId = request_DTO.ClientId,
                            StepStatus = "Approved",
                            UserId = validationResult.Item2,
                            WfStepId = stepData.IdwfSteps
                        };
                        var addWfResult = await _clientWfRepository.AddClientWfStep(newStep);
                        if (addWfResult != null)
                        {
                            resultWithDataBO.Data = 1;
                            resultWithDataBO.IsSuccessful = true;
                            resultWithDataBO.Message = $"Client updated successfully.";
                            _loggerManager.LogInfo(resultWithDataBO.Message);
                        }
                        else 
                        {
                            resultWithDataBO.IsBusinessError = true;
                            resultWithDataBO.BusinessErrorMessage = $"Failed to approve client, Kindly contact administrator.";
                            _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                        }
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to update client, Kindly contact administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else 
                {
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"{validationResult.Item3}.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            }
            else 
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Client data not found.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit ClientService => ApproveClient");
            return resultWithDataBO;
        }



        public async Task<ResultWithDataDTO<GetClientInfoResponse_DTO>> GetClientInfo(int CientId)
        {
            _loggerManager.LogInfo("Entry ClientService => GetClientInfo");
            ResultWithDataDTO<GetClientInfoResponse_DTO> resultWithDataBO = new ResultWithDataDTO<GetClientInfoResponse_DTO>
            {
                IsSuccessful = false
            };
            var existData = await _clientRepository.GetClientInfo(CientId);
            if (existData != null)
            {
                resultWithDataBO.Data = existData;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Client fetched successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Failed to update client, Kindly contact administrator.";
                _loggerManager.LogError(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit ClientService => GetClientInfo");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<List<GetClientInfoResponse_DTO>>> GetClientsByUserId(int userId,int isActive)
        {
            _loggerManager.LogInfo("Entry ClientService => GetClientsByUserId");
            ResultWithDataDTO<List<GetClientInfoResponse_DTO>> resultWithDataBO = new ResultWithDataDTO<List<GetClientInfoResponse_DTO>>
            {
                IsSuccessful = false
            };
            var existData = await _clientRepository.GetClientByUserId(userId,isActive);
            if (existData.Any())
            {
                resultWithDataBO.Data = existData;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Client fetched successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Failed to update client, Kindly contact administrator.";
                _loggerManager.LogError(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit ClientService => GetClientsByUserId");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> RegisterClient(AddClientRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ClientService => RegisterClient");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            using var transaction = await _opsHubContext.Database.BeginTransactionAsync();
            var addResult = await _clientRepository.AddClient(_mapper.Map<UserClient>(request_DTO));
            if (addResult != null)
            {
                var stepData = await _wfStepRepository.GetWfStepByUserId(request_DTO.UserId);
                if (stepData != null)
                {
                    var newStep = new ClientWfSteps()
                    {
                        ClientId = addResult.IduserClient,
                        StepStatus = "Pending",
                        UserId = request_DTO.UserId,
                        WfStepId = stepData.IdwfSteps
                    };
                    var addWfResult = await _clientWfRepository.AddClientWfStep(newStep);
                    if (addWfResult != null)
                    {
                        await transaction.CommitAsync();
                        resultWithDataBO.Data = 1;
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Client registered successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to add client, Kindly contact administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                        throw new Exception(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else 
                {
                    await transaction.RollbackAsync();
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Work flow not found.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            }
            else
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add client, Kindly contact administrator.";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
            }
            _loggerManager.LogInfo("Exit ClientService => RegisterClient");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> SendNotificationToClient(SendClientNotificationRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry ClientService => SendNotificationToClient");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            var existData = await _clientRepository.GetClientInfo(request_DTO.ClientId);
            if (existData != null)
            {
                var validationResult = await ValidateStep(request_DTO.ClientId);
                if (validationResult.Item1)
                {
                    var Module = existData.ModuleName;
                    var UserName = existData.UserName;
                    var userDataForToken = await _clientRepository.GetClientById(request_DTO.ClientId);
                    if (userDataForToken != null)
                    {
                        var notificationResult = await _notificationRepository.SendNotification(userDataForToken.FirebaseToken,
                            $"{Module} Notification",$"Welcome to our {Module} agency by {UserName}"
                            );
                        if (notificationResult)
                        {
                            var stepData = await _wfStepRepository.GetWfStepByUserId(validationResult.Item2);
                            var newStep = new ClientWfSteps()
                            {
                                ClientId = request_DTO.ClientId,
                                StepStatus = "Notified",
                                UserId = validationResult.Item2,
                                WfStepId = stepData.IdwfSteps
                            };
                            var addWfResult = await _clientWfRepository.AddClientWfStep(newStep);
                            if (addWfResult != null)
                            {
                                resultWithDataBO.Data = 1;
                                resultWithDataBO.IsSuccessful = true;
                                resultWithDataBO.Message = $"notification sent successfully.";
                                _loggerManager.LogInfo(resultWithDataBO.Message);
                            }
                            else 
                            {
                                resultWithDataBO.IsSuccessful = true;
                                resultWithDataBO.Message = $"notification not sent.";
                                _loggerManager.LogInfo(resultWithDataBO.Message);
                            }
                        }
                        else 
                        {
                            resultWithDataBO.IsSuccessful = true;
                            resultWithDataBO.Message = $"notification not sent.";
                            _loggerManager.LogInfo(resultWithDataBO.Message);
                        }
                    }
                    else 
                    {
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Client data not found.";
                        _loggerManager.LogError(resultWithDataBO.Message);
                    }
                }
                else
                {
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"{validationResult.Item3}.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Client data not found.";
                _loggerManager.LogError(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit ClientService => SendNotificationToClient");
            return resultWithDataBO;
        }

        public async Task<(bool, int, string)> ValidateStep(int ClientId)
        {
            var clientStep = await _clientWfRepository.GetClientLatestStep(ClientId);
            if (clientStep != null)
            {
                if (clientStep.IsFinalStep != 1)
                {
                    int roleId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("RoleId").Value);
                    int UserId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("UserId").Value);
                    int stepOrderToValidate = clientStep.StepOrder + 1;
                    var nextStep = await _wfStepRepository.GetWfStepByWfIdOrder(clientStep.WorkFlowId, stepOrderToValidate);
                    if (nextStep != null && nextStep.RoleId == roleId)
                    {
                        return (true, UserId, "Role Found.");
                    }
                    else
                    {
                        return (false, 0, "Role not found.");
                    }
                }
                else 
                {
                    return (false,0, "Client already approved and notified");
                }
            }
            else 
            {
                return (false,0,"Invalid step.");
            }
        }
    }
}

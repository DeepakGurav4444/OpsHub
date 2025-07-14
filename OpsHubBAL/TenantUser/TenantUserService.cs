using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDAL.DataModel;
using OpsHubDAL.TenUserRepo;
using OpsHubDAL.UserRoleMapRepo;
using OpsHubDAL.UserRolePerRepo;
using OpsHubDAL.UserRoleRepo;
using OpsHubDTOModel.TenantUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.TenantUser
{
    public class TenantUserService : ITenantUserService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ITenantUserRepository _tenantUserRepository;
        private readonly IUserRolePerRepository _userRolePerRepository;
        private readonly IUserRolMapRepository _userRolMapRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IOpsHubContext _opsHubContext;
        private readonly IMapper _mapper;
        public TenantUserService(ITenantUserRepository tenantUserRepository, IUserRolePerRepository userRolePerRepository, IUserRolMapRepository userRolMapRepository, IUserRoleRepository userRoleRepository, IOpsHubContext opsHubContext, ILoggerManager loggerManager,IMapper mapper) 
        {
            _tenantUserRepository = tenantUserRepository;
            _userRolePerRepository = userRolePerRepository; 
            _userRolMapRepository = userRolMapRepository;
            _userRoleRepository = userRoleRepository; 
            _loggerManager = loggerManager;
            _mapper = mapper;
            _opsHubContext = opsHubContext;
        }

        public async Task<ResultWithDataDTO<int>> AddTenantSuperAdmin(AddTenantSuperAdminRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry TenantUserService => AddTenantSuperAdmin");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            
                var IsExitAdmin = await _tenantUserRepository.IsAdminExist(request_DTO.ModulesId, request_DTO.TenantId);
                if (!IsExitAdmin)
                {
                    //var roleForAdmin = await _userRolePerRepository.GetUserRolePerByModPerId(request_DTO.ModulesId, 1);
                    var roleForAdmin = await _userRoleRepository.GetAdminRoleByModTenId(request_DTO.TenantId, request_DTO.ModulesId);
                    if (roleForAdmin != null)
                    {
                        var isRoleMapped = await _userRolMapRepository.IsMappedRole(roleForAdmin.IduserRoles);
                        if (!isRoleMapped)
                        {
                            var admin = _mapper.Map<TenUsers>(request_DTO);
                            admin.IsAdmin = 1;
                            using var transaction = await _opsHubContext.Database.BeginTransactionAsync();
                            var tenInsResult = await _tenantUserRepository.AddTenUser(admin);
                            if (tenInsResult != null)
                            {
                                var mapRole = await _userRolMapRepository.MapUserRole(new List<UserRoleMap>(){ new UserRoleMap()
                            {
                                RoleId = roleForAdmin.IduserRoles,
                                TenUserId = tenInsResult.IdtenUsers
                            } });
                                if (mapRole != 0)
                                {
                                    await transaction.CommitAsync();
                                    resultWithDataBO.Data = 1;
                                    resultWithDataBO.IsSuccessful = true;
                                    resultWithDataBO.Message = $"Admin added successfully.";
                                    _loggerManager.LogInfo(resultWithDataBO.Message);
                                }
                                else
                                {
                                    resultWithDataBO.IsBusinessError = true;
                                    resultWithDataBO.BusinessErrorMessage = $"Failed to map user role, Kindly contact administrator.";
                                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                                }
                            }
                            else
                            {
                                await transaction.RollbackAsync();
                                resultWithDataBO.IsBusinessError = true;
                                resultWithDataBO.BusinessErrorMessage = $"Failed to add tenant admin, Kindly contact administrator.";
                                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                                throw new Exception(resultWithDataBO.BusinessErrorMessage);
                            }
                        }
                        else
                        {
                            resultWithDataBO.IsSuccessful = true;
                            resultWithDataBO.Message = $"Admin already assigned.";
                            _loggerManager.LogInfo(resultWithDataBO.Message);
                        }
                    }
                    else
                    {
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Role not found.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                }
                else
                {
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Admin alreday exist.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            _loggerManager.LogInfo("Exit TenantUserService => AddTenantSuperAdmin");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> AddTenantUser(AddTenantUserRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry TenantUserService => AddTenantUser");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            var isRoleMapped = await _userRolMapRepository.IsMappedRole(request_DTO.RoleId);
            if (!isRoleMapped)
            {
                var user = _mapper.Map<TenUsers>(request_DTO);
                using var transaction = await _opsHubContext.Database.BeginTransactionAsync();
                var tenInsResult = await _tenantUserRepository.AddTenUser(user);
                if (tenInsResult != null)
                {
                    var mapRole = await _userRolMapRepository.MapUserRole(new List<UserRoleMap>(){ new UserRoleMap()
                    {
                        RoleId = request_DTO.RoleId,
                        TenUserId = tenInsResult.IdtenUsers
                    } });
                    if (mapRole != 0)
                    {
                        await transaction.CommitAsync();
                        resultWithDataBO.Data = 1;
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"User added successfully.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to map user role, Kindly contact administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to add tenant user, Kindly contact administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    throw new Exception(resultWithDataBO.BusinessErrorMessage);
                }
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Another User already assigned for this role.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit TenantUserService => AddTenantUser");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> AsignRoleToUser(AsignRoleToUserRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry TenantUserService => AsignRoleToUser");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            var isUerExist = await _tenantUserRepository.GetTenUserById(request_DTO.UserId);
            if (isUerExist != null) 
            { 
                var isRoleMapped = await _userRolMapRepository.IsMappedRole(request_DTO.RoleId);
                if (!isRoleMapped)
                {
                    var mapRole = await _userRolMapRepository.MapUserRole(new List<UserRoleMap>(){ new UserRoleMap()
                    {
                        RoleId = request_DTO.RoleId,
                        TenUserId = request_DTO.UserId
                    } });
                    if (mapRole != 0)
                    {
                        resultWithDataBO.Data = 1;
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Usern role mapped.";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to map user role, Kindly contact administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else 
                {
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"User not exist.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Another User already assigned for this role.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit TenantUserService => AsignRoleToUser");
            return resultWithDataBO;
        }
    }
}

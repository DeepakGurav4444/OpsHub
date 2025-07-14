using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDAL.DataModel;
using OpsHubDAL.JWTService;
using OpsHubDAL.SysAdminRepo;
using OpsHubDAL.TenantRepo;
using OpsHubDAL.TenUserRepo;
using OpsHubDAL.UserRoleMapRepo;
using OpsHubDAL.UserRolePerRepo;
using OpsHubDTOModel.Login;
using OpsHubDTOModel.SubPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ITenantUserRepository _tenantUserRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly ISysAdminRepository _sysAdminRepository;
        private readonly IUserRolePerRepository _userRolePerRepository;
        private readonly IUserRolMapRepository _userRolMapRepository;
        private readonly IJWTService _jwtService;
        public LoginService(ILoggerManager loggerManager,ITenantUserRepository tenantUserRepository,ITenantRepository tenantRepository, IJWTService jwtService, ISysAdminRepository sysAdminRepository, IUserRolePerRepository userRolePerRepository, IUserRolMapRepository userRolMapRepository) 
        {
            _loggerManager = loggerManager;
            _tenantUserRepository = tenantUserRepository;
            _tenantRepository = tenantRepository;
            _sysAdminRepository = sysAdminRepository;
            _userRolePerRepository = userRolePerRepository;
            _userRolMapRepository = userRolMapRepository;
            _jwtService = jwtService;
        }
        public async Task<ResultWithDataDTO<LoginResponse_DTO>> LoginTenant(LoginAdminRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LoginService => LoginTenant");
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataBO = new ResultWithDataDTO<LoginResponse_DTO>
            {
                IsSuccessful = false
            };

            var tenantData = await _tenantRepository.GetTenantByEmailPass(request_DTO.EmailId,request_DTO.Password);
            if (tenantData != null)
            {
                var token = _jwtService.GenerateToken("Tenant", tenantData.IdtenTenants, tenantData.IdtenTenants, 0, 0, new List<string>() { "create_admin", "subscribe_plan" });
                var existToken = await _jwtService.GetRefreshToken(tenantData.IdtenTenants, "Tenant",request_DTO.deviceId);
                var refreshToken = _jwtService.GenerateRefreshToken();
                if (existToken != null)
                {
                    existToken.IsUsed = true;
                    var updateResult = await _jwtService.UpdateRefreshToken(existToken);
                }
                var insertRefToken = await _jwtService.AddRefreshToken(new RefreshTokens() 
                {
                    DeviceId = request_DTO.deviceId,
                    Token = refreshToken,
                    UserType = "Tenant",
                    UserId = tenantData.IdtenTenants,
                    ExpiresAt = DateTime.UtcNow.AddMonths(1),

                }); 
                var dataResult = new LoginResponse_DTO() 
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
                resultWithDataBO.Data = dataResult;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Logged in successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plans are not present.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit LoginService => LoginTenant");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<LoginResponse_DTO>> LoginUser(LoginAdminRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LoginService => LoginUser");
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataBO = new ResultWithDataDTO<LoginResponse_DTO>
            {
                IsSuccessful = false
            };
            var tenantUserData = await _tenantUserRepository.GetUserByEmailPass(request_DTO.EmailId, request_DTO.Password);
            if (tenantUserData != null)
            {
                var role = await _userRolMapRepository.GetUserRole(tenantUserData.IdtenUsers);
                var permissions = await _userRolePerRepository.GetUserPermissionsByUserId(tenantUserData.IdtenUsers);
                var token = _jwtService.GenerateToken("TenantUser", tenantUserData.IdtenUsers, tenantUserData.TenantId, tenantUserData.ModulesId, role.RoleId, permissions);
                var existToken = await _jwtService.GetRefreshToken(tenantUserData.IdtenUsers, "TenantUser", request_DTO.deviceId);
                var refreshToken = _jwtService.GenerateRefreshToken();
                if (existToken != null)
                {
                    existToken.IsUsed = true;
                    var updateResult = await _jwtService.UpdateRefreshToken(existToken);
                }
                var insertRefToken = await _jwtService.AddRefreshToken(new RefreshTokens()
                {
                    DeviceId = request_DTO.deviceId,
                    Token = refreshToken,
                    UserType = "TenantUser",
                    UserId = tenantUserData.IdtenUsers,
                    ExpiresAt = DateTime.UtcNow.AddMonths(1),
                });
                var dataResult = new LoginResponse_DTO()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
                resultWithDataBO.Data = dataResult;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Logged in successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plans are not present.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit LoginService => LoginUser");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<LoginResponse_DTO>> LoginSysAdmin(LoginAdminRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LoginService => LoginSysAdmin");
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataBO = new ResultWithDataDTO<LoginResponse_DTO>
            {
                IsSuccessful = false
            };
            var syAdminData = await _sysAdminRepository.GetAdminByEmailPass(request_DTO.EmailId, request_DTO.Password);
            if (syAdminData != null)
            {
                var token = _jwtService.GenerateToken("SystemAdmin", syAdminData.IdsysAdmins, 0, 0, 0, new List<string>() { "create_tenant" });
                var existToken = await _jwtService.GetRefreshToken(syAdminData.IdsysAdmins, "SystemAdmin", request_DTO.deviceId);
                var refreshToken = _jwtService.GenerateRefreshToken();
                if (existToken != null)
                {
                    existToken.IsUsed = true;
                    var updateResult = await _jwtService.UpdateRefreshToken(existToken);
                }
                var insertRefToken = await _jwtService.AddRefreshToken(new RefreshTokens()
                {
                    DeviceId = request_DTO.deviceId,
                    Token = refreshToken,
                    UserType = "SystemAdmin",
                    UserId = syAdminData.IdsysAdmins,
                    ExpiresAt = DateTime.UtcNow.AddMonths(1),
                });
                var dataResult = new LoginResponse_DTO()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
                resultWithDataBO.Data = dataResult;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Logged in successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plans are not present.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit LoginService => LoginSysAdmin");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<LoginResponse_DTO>> RefreshToken(RefreshTokenRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LoginService => RefreshToken");
            ResultWithDataDTO<LoginResponse_DTO> resultWithDataBO = new ResultWithDataDTO<LoginResponse_DTO>
            {
                IsSuccessful = false
            };
            RefreshTokens newToken;
            string token = string.Empty;
            var existToken = await _jwtService.GetRefreshTokenStatus(request_DTO.UserId, request_DTO.UserType, request_DTO.DeviceId,request_DTO.RefreshToken);
            if (existToken == null)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            else 
            {
                if (request_DTO.UserType == "SystemAdmin")
                {

                    token = _jwtService.GenerateToken(request_DTO.UserType, request_DTO.UserId, 0, 0, 0, new List<string>() { "create_admin" });
                }
                else if (request_DTO.UserType == "Tenant")
                {

                    token = _jwtService.GenerateToken(request_DTO.UserType, request_DTO.UserId, 0, 0, 0, new List<string>() { "create_user" });
                }
                else
                {
                    var permissions = await _userRolePerRepository.GetUserPermissionsByUserId(request_DTO.UserId);
                    var role = await _userRolMapRepository.GetUserRole(request_DTO.UserId);
                    if (permissions.Any() && role != null)
                    {
                        token = _jwtService.GenerateToken(request_DTO.UserType, request_DTO.UserId, 0, 0, role.RoleId, permissions);
                    }
                    else 
                    {
                        resultWithDataBO.Message = "Access token not generated becuase role or permissions not found.";
                    }
                }
                if (!string.IsNullOrEmpty(token))
                {
                    string refreshToken = _jwtService.GenerateRefreshToken();
                    newToken = new RefreshTokens()
                    {
                        DeviceId = request_DTO.DeviceId,
                        UserType = request_DTO.UserType,
                        Token = refreshToken,
                        ExpiresAt = DateTime.UtcNow.AddMonths(1),
                        UserId = request_DTO.UserId
                    };
                    var storeRefToken = await _jwtService.AddRefreshToken(newToken);
                    existToken.IsUsed = true;
                    var updateResult = await _jwtService.UpdateRefreshToken(existToken);
                    var dataResult = new LoginResponse_DTO()
                    {
                        AccessToken = token,
                        RefreshToken = refreshToken
                    };
                    resultWithDataBO.Data = dataResult;
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"refresh token updated.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else 
                {
                    resultWithDataBO.IsSuccessful = false;
                    _loggerManager.LogError(resultWithDataBO.Message);
                }
            }
            _loggerManager.LogInfo("Exit LoginService => LoginSysAdmin");
            return resultWithDataBO;
        }
    }
}

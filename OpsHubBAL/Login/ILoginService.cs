using Microsoft.AspNetCore.Identity.Data;
using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Login
{
    public interface ILoginService
    {
        Task<ResultWithDataDTO<LoginResponse_DTO>> LoginUser(LoginAdminRequest_DTO request_DTO); 
        Task<ResultWithDataDTO<LoginResponse_DTO>> LoginTenant(LoginAdminRequest_DTO request_DTO);
        Task<ResultWithDataDTO<LoginResponse_DTO>> LoginSysAdmin(LoginAdminRequest_DTO request_DTO);
        Task<ResultWithDataDTO<LoginResponse_DTO>> RefreshToken(RefreshTokenRequest_DTO request_DTO);
    }
}

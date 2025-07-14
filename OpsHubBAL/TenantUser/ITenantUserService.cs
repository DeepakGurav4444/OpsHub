using OpsHubCommonUtility.Response;
using OpsHubDTOModel.TenantUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.TenantUser
{
    public interface ITenantUserService
    {
        Task<ResultWithDataDTO<int>> AddTenantUser(AddTenantUserRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> AddTenantSuperAdmin(AddTenantSuperAdminRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> AsignRoleToUser(AsignRoleToUserRequest_DTO request_DTO);
    }
}

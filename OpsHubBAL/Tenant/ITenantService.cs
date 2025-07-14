using OpsHubCommonUtility.Response;
using OpsHubDTOModel.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Tenant
{
    public interface ITenantService
    {
        Task<ResultWithDataDTO<int>> RegisterTenant(AddTenantsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> SubscribeToPlan(SubscribePlanRequest_DTO request_DTO);
    }
}

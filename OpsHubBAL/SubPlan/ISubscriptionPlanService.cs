using OpsHubCommonUtility.Response;
using OpsHubDTOModel.SubPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.SubPlan
{
    public interface ISubscriptionPlanService
    {
        Task<ResultWithDataDTO<List<GetSubPlansResponse_DTO>>> GetSubPlan();
    }
}

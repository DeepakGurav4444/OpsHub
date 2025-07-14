using OpsHubDAL.DataModel;
using OpsHubDTOModel.SubPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SubPlan
{
    public interface ISubScriptionPlanRepository
    {
        Task<List<GetSubPlansResponse_DTO>> GetSubPlans();
        Task<SubPlans> GetSubPlanById(int SubPlanId);
    }
}

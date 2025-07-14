using System;
using System.Collections.Generic;
using System.Linq;
using OpsHubDAL.DataModel;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.SubPlanModMap
{
    public interface ISubPlanModMapRepository
    {
        Task<List<SubModMap>> GetSubPlanModMapBySubId(int SubPlanId);
        Task<List<SubModMap>> GetSubPlanModListBySubId(List<int> SubPlanIdList);
    }
}

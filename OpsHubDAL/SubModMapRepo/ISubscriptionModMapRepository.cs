using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SubModMapRepo
{
    public interface ISubscriptionModMapRepository
    {
        Task<List<SubModMap>> GetMappedModulesBySubId(int SubPlanId);
    }
}

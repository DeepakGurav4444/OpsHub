using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenSubMapRepo
{
    public interface ITenantSubMapRepository
    {
        Task<List<TenSubMap>> GetTenSubMap(int tenantId);
        Task<TenSubMap> AddTenSubPlan(TenSubMap tenSubMap);
    }
}

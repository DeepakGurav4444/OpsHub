using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenModRepo
{
    public interface ITenantModuleRepository
    {
        Task<int> AddTenantModules(List<TenModules> tenModules);
        Task<List<TenModules>> GetTenantModulesById(int tenantId);
    }
}

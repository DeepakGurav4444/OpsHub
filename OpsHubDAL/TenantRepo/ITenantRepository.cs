using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenantRepo
{
    public interface ITenantRepository
    {
        Task<TenTenants> AddTenant(TenTenants tenTenants);
        Task<TenTenants> GetTenantByEmailPass(string EmailId, string Password);
    }
}

using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.TenUserRepo
{
    public interface ITenantUserRepository
    {
        Task<TenUsers> AddTenUser(TenUsers tenUser);
        Task<bool> IsAdminExist(int ModuleId,int TenantId);
        Task<TenUsers> GetUserByEmailPass(string EmailId, string Password);
        Task<TenUsers> GetTenUserById(int UserId);
    }
}

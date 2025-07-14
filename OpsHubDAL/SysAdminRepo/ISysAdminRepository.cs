using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SysAdminRepo
{
    public interface ISysAdminRepository
    {
        Task<SysAdmins> GetAdminByEmailPass(string emailId,string password);
    }
}

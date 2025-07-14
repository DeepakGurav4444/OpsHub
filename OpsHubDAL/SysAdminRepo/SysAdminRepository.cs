using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.SysAdminRepo
{
    public class SysAdminRepository : ISysAdminRepository
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IOpsHubContext _opsHubContext;
        public SysAdminRepository(ILoggerManager loggerManager,IOpsHubContext opsHubContext) 
        {
            _loggerManager = loggerManager; 
            _opsHubContext = opsHubContext;
        }

        public async Task<SysAdmins> GetAdminByEmailPass(string emailId, string password)
        {
            _loggerManager.LogInfo("Entry SysAdminRepository => GetAdminByEmailPass");
            var dataResult = await _opsHubContext.SysAdmins.FirstOrDefaultAsync(x=>x.EmailId == emailId && x.UserPass == password && x.IsActive==1);
            _loggerManager.LogInfo("Exit SysAdminRepository => GetAdminByEmailPass");
            return dataResult;
        }
    }
}

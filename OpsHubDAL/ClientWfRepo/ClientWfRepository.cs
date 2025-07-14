using Microsoft.EntityFrameworkCore;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.ClientWfRepo
{
    public class ClientWfRepository : IClientWfRepository
    {
        private readonly IOpsHubContext _opsHubContext;
        private readonly ILoggerManager _loggerManager;
        public ClientWfRepository(IOpsHubContext opsHubContext, ILoggerManager loggerManager) 
        { 
            _opsHubContext = opsHubContext;
            _loggerManager = loggerManager;
        }
        public async Task<ClientWfSteps> AddClientWfStep(ClientWfSteps clientWfSteps)
        {
            _loggerManager.LogInfo("Entry ClientWfRepository => AddClientWfStep");
            var dataResult = await _opsHubContext.ClientWfSteps.AddAsync(clientWfSteps);
            await _opsHubContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit ClientWfRepository => AddClientWfStep");
            return dataResult.Entity;
        }

        public async Task<WfSteps> GetClientLatestStep(int ClientId)
        {
            _loggerManager.LogInfo("Entry ClientWfRepository => GetClientLatestStatus");
            var dataResult = await
                (from cwf in _opsHubContext.ClientWfSteps
                 join wfs in _opsHubContext.WfSteps on cwf.WfStepId equals wfs.IdwfSteps
                 where cwf.ClientId == ClientId
                 orderby cwf.CompletedAt descending
                 select wfs).FirstOrDefaultAsync();                
            _loggerManager.LogInfo("Exit ClientWfRepository => GetClientLatestStatus");
            return dataResult;
        }
    }
}

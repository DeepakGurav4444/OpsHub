using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.ClientWfRepo
{
    public interface IClientWfRepository
    {
        Task<ClientWfSteps> AddClientWfStep(ClientWfSteps clientWfSteps);
        Task<WfSteps> GetClientLatestStep(int ClientId);
    }
}

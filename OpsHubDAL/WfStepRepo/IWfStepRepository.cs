using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.WfStepRepo
{
    public interface IWfStepRepository
    {
        Task<WfSteps> GetWfStepByUserId(int UserId);
        Task<WfSteps> GetWfStepByWfIdOrder(int wfId, int stepOrder);
    }
}

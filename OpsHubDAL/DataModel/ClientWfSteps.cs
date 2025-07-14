using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class ClientWfSteps
{
    public int IdclientWfStep { get; set; }

    public int ClientId { get; set; }

    public int WfStepId { get; set; }

    public int UserId { get; set; }

    public string StepStatus { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual UserClient Client { get; set; }

    public virtual TenUsers User { get; set; }

    public virtual WfSteps WfStep { get; set; }
}

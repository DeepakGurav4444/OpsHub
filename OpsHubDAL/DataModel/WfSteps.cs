using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class WfSteps
{
    public int IdwfSteps { get; set; }

    public int WorkFlowId { get; set; }

    public string StepName { get; set; }

    public int StepOrder { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public sbyte IsFinalStep { get; set; }

    public virtual ICollection<ClientWfSteps> ClientWfSteps { get; set; } = new List<ClientWfSteps>();

    public virtual UserPermissions Permission { get; set; }

    public virtual UserRoles Role { get; set; }

    public virtual Wf WorkFlow { get; set; }
}

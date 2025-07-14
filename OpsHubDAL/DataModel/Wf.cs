using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class Wf
{
    public int Idwf { get; set; }

    public int ModuleId { get; set; }

    public int TenantId { get; set; }

    public string WfName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ModModules Module { get; set; }

    public virtual TenTenants Tenant { get; set; }

    public virtual ICollection<WfSteps> WfSteps { get; set; } = new List<WfSteps>();
}

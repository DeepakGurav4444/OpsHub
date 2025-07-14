using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class SubModMap
{
    public int IdsubModMap { get; set; }

    public int SubPlanId { get; set; }

    public int ModuleId { get; set; }

    public int? IsActive { get; set; }

    public virtual ModModules Module { get; set; }

    public virtual SubPlans SubPlan { get; set; }
}

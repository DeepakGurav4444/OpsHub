using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class TenSubMap
{
    public int IdtenSubMap { get; set; }

    public int TenantId { get; set; }

    public int SubPlanId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual SubPlans SubPlan { get; set; }

    public virtual TenTenants Tenant { get; set; }
}

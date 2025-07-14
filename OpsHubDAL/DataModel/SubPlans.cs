using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class SubPlans
{
    public int IdsubPlans { get; set; }

    public string PlanName { get; set; }

    public int DurationInMonth { get; set; }

    public decimal Price { get; set; }

    public int MaxUsers { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<SubModMap> SubModMap { get; set; } = new List<SubModMap>();

    public virtual ICollection<TenSubMap> TenSubMap { get; set; } = new List<TenSubMap>();
}

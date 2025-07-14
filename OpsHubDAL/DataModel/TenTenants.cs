using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class TenTenants
{
    public int IdtenTenants { get; set; }

    public string TenantName { get; set; }

    public string EmailId { get; set; }

    public string Domain { get; set; }

    public string LogoPath { get; set; }

    public string TenPass { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<TenModules> TenModules { get; set; } = new List<TenModules>();

    public virtual ICollection<TenSubMap> TenSubMap { get; set; } = new List<TenSubMap>();

    public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

    public virtual ICollection<Wf> Wf { get; set; } = new List<Wf>();
}

using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class ModModules
{
    public int IdmodModules { get; set; }

    public string ModuleName { get; set; }

    public string Description { get; set; }

    public string IconUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<SubModMap> SubModMap { get; set; } = new List<SubModMap>();

    public virtual ICollection<TenModules> TenModules { get; set; } = new List<TenModules>();

    public virtual ICollection<TenUsers> TenUsers { get; set; } = new List<TenUsers>();

    public virtual ICollection<UserClient> UserClient { get; set; } = new List<UserClient>();

    public virtual ICollection<UserRolePermissions> UserRolePermissions { get; set; } = new List<UserRolePermissions>();

    public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

    public virtual ICollection<Wf> Wf { get; set; } = new List<Wf>();
}

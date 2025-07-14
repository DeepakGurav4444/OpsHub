using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class UserPermissions
{
    public int IduserPermissions { get; set; }

    public string PermissionName { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<UserRolePermissions> UserRolePermissions { get; set; } = new List<UserRolePermissions>();

    public virtual ICollection<WfSteps> WfSteps { get; set; } = new List<WfSteps>();
}

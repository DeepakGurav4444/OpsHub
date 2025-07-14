using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class UserRoles
{
    public int IduserRoles { get; set; }

    public int TenantId { get; set; }

    public int ModuleId { get; set; }

    public string RoleName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public sbyte IsForAdmin { get; set; }

    public int? IsActive { get; set; }

    public virtual ModModules Module { get; set; }

    public virtual TenTenants Tenant { get; set; }

    public virtual ICollection<UserRoleMap> UserRoleMap { get; set; } = new List<UserRoleMap>();

    public virtual ICollection<UserRolePermissions> UserRolePermissions { get; set; } = new List<UserRolePermissions>();

    public virtual ICollection<WfSteps> WfSteps { get; set; } = new List<WfSteps>();
}

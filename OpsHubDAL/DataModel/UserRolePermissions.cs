using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class UserRolePermissions
{
    public int IduserRolePermissions { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public int ModuleId { get; set; }

    public int? IsActive { get; set; }

    public virtual ModModules Module { get; set; }

    public virtual UserPermissions Permission { get; set; }

    public virtual UserRoles Role { get; set; }
}

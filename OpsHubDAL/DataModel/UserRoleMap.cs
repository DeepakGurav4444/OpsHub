using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class UserRoleMap
{
    public int IduserRoleMap { get; set; }

    public int TenUserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual UserRoles Role { get; set; }

    public virtual TenUsers TenUser { get; set; }
}

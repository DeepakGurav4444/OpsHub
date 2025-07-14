using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class RoleRoles
{
    public int IdroleRoles { get; set; }

    public string RoleName { get; set; }

    public string Description { get; set; }

    public sbyte? IsForAdmin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public sbyte? IsActive { get; set; }
}

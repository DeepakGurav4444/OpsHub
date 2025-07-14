using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class TenUsers
{
    public int IdtenUsers { get; set; }

    public int? PrincipalId { get; set; }

    public int TenantId { get; set; }

    public int ModulesId { get; set; }

    public string TenUserName { get; set; }

    public string EmailId { get; set; }

    public string TenUserPass { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public sbyte? IsAdmin { get; set; }

    public virtual ICollection<ClientWfSteps> ClientWfSteps { get; set; } = new List<ClientWfSteps>();

    public virtual ICollection<TenUsers> InversePrincipal { get; set; } = new List<TenUsers>();

    public virtual ModModules Modules { get; set; }

    public virtual TenUsers Principal { get; set; }

    public virtual ICollection<UserClient> UserClient { get; set; } = new List<UserClient>();

    public virtual ICollection<UserRoleMap> UserRoleMap { get; set; } = new List<UserRoleMap>();
}

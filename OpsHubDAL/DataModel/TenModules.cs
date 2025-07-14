using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class TenModules
{
    public int IdtenModules { get; set; }

    public int TenantId { get; set; }

    public int ModulesId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public DateTime ExpiresOn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ModModules Modules { get; set; }

    public virtual TenTenants Tenant { get; set; }
}

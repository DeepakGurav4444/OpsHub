using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class UserClient
{
    public int IduserClient { get; set; }

    public string ClientName { get; set; }

    public int UserId { get; set; }

    public int ModuleId { get; set; }

    public string FirebaseToken { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<ClientWfSteps> ClientWfSteps { get; set; } = new List<ClientWfSteps>();

    public virtual ModModules Module { get; set; }

    public virtual TenUsers User { get; set; }
}

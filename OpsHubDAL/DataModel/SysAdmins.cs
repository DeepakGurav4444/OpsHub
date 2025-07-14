using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class SysAdmins
{
    public int IdsysAdmins { get; set; }

    public string UserName { get; set; }

    public string EmailId { get; set; }

    public string UserPass { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsActive { get; set; }
}

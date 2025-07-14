using System;
using System.Collections.Generic;

namespace OpsHubDAL.DataModel;

public partial class RefreshTokens
{
    public int IdRefreshToken { get; set; }

    public int UserId { get; set; }

    public string UserType { get; set; }

    public string Token { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string DeviceId { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsRevoked { get; set; }
}

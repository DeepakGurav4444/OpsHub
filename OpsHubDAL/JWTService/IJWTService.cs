using OpsHubDAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDAL.JWTService
{
    public interface IJWTService
    {
        string GenerateToken(string type, int UserId, int TenantId, int ModuleId, int roleId, List<string> permissions);
        string GenerateRefreshToken();
        Task<RefreshTokens> AddRefreshToken(RefreshTokens token);
        Task<RefreshTokens> UpdateRefreshToken(RefreshTokens token);
        Task<RefreshTokens> GetRefreshToken(int UserId, string Type, string deviceId);
        Task<RefreshTokens> GetRefreshTokenStatus(int UserId, string Type, string deviceId,string refreshToken);
    }
}

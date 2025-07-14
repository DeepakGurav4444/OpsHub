using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Login
{
    public class LoginResponse_DTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

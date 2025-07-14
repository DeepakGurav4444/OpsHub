using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Client
{
    public class GetClientInfoResponse_DTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

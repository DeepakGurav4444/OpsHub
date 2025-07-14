using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Client
{
    public class ApproveClientRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid ClientId.")]
        public int ClientId { get; set; }
        public int UserId { get; set; }
    }
}

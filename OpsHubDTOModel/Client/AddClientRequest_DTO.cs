using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Client
{
    public class AddClientRequest_DTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string ClientName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid UserId.")]
        public int UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid ModuleId.")]
        public int ModuleId { get; set; }
    }
}

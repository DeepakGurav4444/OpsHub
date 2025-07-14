using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Tenant
{
    public class AddTenantsRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid System Admin Id.")]
        public int SysAdminId { get; set; }
        [Required(ErrorMessage = "TenantName is Required.")]
        public string TenantName { get; set; }
        [Required(ErrorMessage = "Domain is Required.")]
        public string Domain { get; set; }
        [Required(ErrorMessage = "LogoPath is Required.")]
        public string LogoPath { get; set; }
        [Required(ErrorMessage = "EmailId is Required.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "TenPass is Required.")]
        public string TenPass { get; set; }
    }
}

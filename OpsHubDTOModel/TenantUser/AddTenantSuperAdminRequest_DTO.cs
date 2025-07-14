using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.TenantUser
{
    public class AddTenantSuperAdminRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid TenantId.")]
        public int TenantId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid ModulesId.")]
        public int ModulesId { get; set; }
        [Required(ErrorMessage = "TenUserName is required.")]
        public string TenUserName { get; set; }
        [Required(ErrorMessage = "EmailId is required.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "TenUserPass is required.")]
        public string TenUserPass { get; set; }

    }
}

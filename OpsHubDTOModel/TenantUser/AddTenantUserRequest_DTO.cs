using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.TenantUser
{
    public class AddTenantUserRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid RoleId.")]
        public int RoleId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid PrincipalId.")]
        public int PrincipalId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid TenantId.")]
        public int TenantId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid ModulesId.")]
        public int ModulesId { get; set; }
        [Required(ErrorMessage = "TenUserName is Required.")]
        public string TenUserName { get; set; }
        [Required(ErrorMessage = "EmailId is Required.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "TenUserPass is Required.")]
        public string TenUserPass { get; set; }
    }
}

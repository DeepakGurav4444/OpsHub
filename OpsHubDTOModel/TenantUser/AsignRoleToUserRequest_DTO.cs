using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.TenantUser
{
    public class AsignRoleToUserRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid UserId.")]
        public int UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid RoleId.")]
        public int RoleId { get; set; }
    }
}

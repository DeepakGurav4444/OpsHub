using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Tenant
{
    public class SubscribePlanRequest_DTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid TenantId.")]
        public int TenantId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid SubPlanId.")]
        public int SubPlanId { get; set; }
    }
}

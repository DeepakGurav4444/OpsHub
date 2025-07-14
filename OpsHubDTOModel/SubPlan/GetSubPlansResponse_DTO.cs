using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.SubPlan
{
    public class GetSubPlansResponse_DTO
    {
        public int IdsubPlans { get; set; }
        public string PlanName { get; set; }
        public int DurationInMonth { get; set; }
        public decimal Price { get; set; }
        public int MaxUsers { get; set; }
        public List<Modules> Modules { get; set; }
    }

    public class Modules 
    {
        public int IdmodModules { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}

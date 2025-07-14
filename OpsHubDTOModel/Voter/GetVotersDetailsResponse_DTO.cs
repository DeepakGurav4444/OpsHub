using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Voter
{
    public class GetVotersDetailsResponse_DTO
    {
        public int IdvoterData { get; set; }

        public string VoterCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string AlternetContactNumber { get; set; }

        public DateOnly? Dob { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public int StateMasterId { get; set; }

        public string StateName { get; set; }

        public int DistrictMasterId { get; set; }

        public string DistrictName { get; set; }

        public int? AssemblyConsId { get; set; }

        public string Assembly { get; set; } 

        public int? ParliamentaryConsId { get; set; }

        public string Parliament { get; set; }

        public string Pincode { get; set; }

        public string VoterId { get; set; }

        public string AadharNumber { get; set; }

        public string PanNumber { get; set; }

        public string Qualification { get; set; }

        public string Profession { get; set; }

        public string Religion { get; set; }

        public string Caste { get; set; }

        public string Category { get; set; }

        public string ReferCode { get; set; }

        public string Zilla { get; set; }

        public string Mandal { get; set; }

        public string Booth { get; set; }

        public string WardName { get; set; }

        public string WhNumber { get; set; }

        public string FbUrl { get; set; }

        public string InstaUrl { get; set; }

        public string XUrl { get; set; }

        public sbyte? IsActive { get; set; }
    }
}

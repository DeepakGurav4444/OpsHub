using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Voter
{
    public class RegisterVoterRequest_DTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string AlternetContactNumber { get; set; }

        public DateOnly? Dob { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public int StateMasterId { get; set; }

        public int DistrictMasterId { get; set; }

        public int AssemblyConsId { get; set; }

        public int ParliamentaryConsId { get; set; }

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
        public List<FamilyDetails_DTO> FamilyDetails { get; set; }
        public int IsActive { get; set; }
    }

    public class FamilyDetails_DTO 
    {
        public string FullName { get; set; }

        public string Gender { get; set; }
        public int VoterId { get; set; }

        public string Relationship { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly? Dob { get; set; }
        public int? IsActive { get; set; }

    }
}

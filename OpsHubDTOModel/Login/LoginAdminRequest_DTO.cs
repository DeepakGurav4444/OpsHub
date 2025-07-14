using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Login
{
    public class LoginAdminRequest_DTO
    {
        [Required(ErrorMessage = "EmailId is Required.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "deviceId is Required.")]
        public string deviceId { get; set; }
    }
}

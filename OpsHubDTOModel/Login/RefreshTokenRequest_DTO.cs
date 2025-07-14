using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubDTOModel.Login
{
    public class RefreshTokenRequest_DTO
    {
        [Required(ErrorMessage = "RefreshToken is Required.")]
        public string RefreshToken {  get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Enter valid UserId.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "UserType is Required.")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "DeviceId is Required.")]
        public string DeviceId { get; set; }
    }
}

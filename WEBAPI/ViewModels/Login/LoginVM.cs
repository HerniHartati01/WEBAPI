using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Login
{
    public class LoginVM
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

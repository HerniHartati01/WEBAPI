using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Employee;

namespace WEBAPI.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public int Otp { get; set; }

        
        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }




    }
}

using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Accounts
{
    public class AccountEmpVM
    {
        [EmailAddress]
        [EmailPhoneNikValidation(nameof(Email))]
        public string Email { get; set; }

        [PasswordValidation(ErrorMessage = "Password must contain at least 1 Uppercase, 1 Lower Case, 1 Symbol, 1 Number")]
        public string Password { get; set; }
    }
}

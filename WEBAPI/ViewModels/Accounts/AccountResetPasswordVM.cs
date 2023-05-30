using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Accounts
{
    public class AccountResetPasswordVM
    {
        public int OTP { get; set; }
        [EmailAddress]
        [EmailPhoneNikValidation(nameof(Email))]
        public string Email { get; set; }
    }
}

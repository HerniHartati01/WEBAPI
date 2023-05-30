using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Employee;

namespace WEBAPI.ViewModels.Accounts
{
    public class AccountVM 
    {
        public Guid? Guid { get; set; }
        [PasswordValidation(ErrorMessage = "Password must contain at least 1 Uppercase, 1 Lower Case, 1 Symbol, 1 Number")]
        public string Password { get; set; }
     
        public bool IsDeleted { get; set; }
      
        public int Otp { get; set; }
       
        public bool IsUsed { get; set; }
      
        public DateTime ExpiredTime { get; set; }

       
    }
}

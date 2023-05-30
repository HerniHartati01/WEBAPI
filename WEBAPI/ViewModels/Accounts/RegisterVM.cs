using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Accounts
{
    public class RegisterVM
    {
        //public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }
        [EmailAddress]
        [EmailPhoneNikValidation(nameof(Email))]
        public string Email { get; set; }

        [Phone]
        [EmailPhoneNikValidation(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0,4 , ErrorMessage = "Input a Valid number")]
        public float GPA { get; set; }

        //public Guid UniversityGuid { get; set; }

        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        [PasswordValidation(ErrorMessage ="Password must contain at least 1 Uppercase, 1 Lower Case, 1 Symbol, 1 Number")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        // public University? University { get; set; }
    }
}

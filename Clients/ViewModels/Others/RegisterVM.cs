using Clients.ViewModels.Utils;
using System.ComponentModel.DataAnnotations;

namespace Clients.ViewModels.Others
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0, 4, ErrorMessage = "Input a Valid number")]
        public float GPA { get; set; }

        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Employee
{
    public class MasterEmployeeVM
    {
        public Guid? Guid { get; set; }
        public string NIK { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime HiringDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        [Range(0,4, ErrorMessage = "Input a Valid number")]
        public float GPA { get; set; }
        public string UniversityName { get; set; }
    }
}

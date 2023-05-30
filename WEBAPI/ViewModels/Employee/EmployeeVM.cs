using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Employee
{
    public class EmployeeVM
    {
        public Guid? Guid { get; set; }
        public string Nik { get; set; }
       
        public string FirstName { get; set; }
       
        public string? LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
       
        public GenderLevel Gender { get; set; }
       
        public DateTime HiringDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}

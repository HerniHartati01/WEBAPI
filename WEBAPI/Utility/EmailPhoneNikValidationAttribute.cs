using System.ComponentModel.DataAnnotations;
using WEBAPI.Contracts;

namespace WEBAPI.Utility
{
    public class EmailPhoneNikValidationAttribute : ValidationAttribute
    {
       
        private readonly string _propertyName;

        public EmailPhoneNikValidationAttribute(string propertyName)
        {
            _propertyName = propertyName;
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository)) as IEmployeeRepository;

            bool checkEmailAndPhone = employeeRepository.CheckEmailAndPhoneAndNik(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        }
    }
}

using WEBAPI.Models;

namespace WEBAPI.ViewModels.Educations
{
    public class EducationVM
    {
       public Guid? Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static EducationVM ToVM(Education education)
        {
            return new EducationVM
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };
        }
    }
}

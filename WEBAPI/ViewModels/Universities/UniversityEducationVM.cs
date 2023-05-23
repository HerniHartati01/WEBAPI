using WEBAPI.ViewModels.Educations;

namespace WEBAPI.ViewModels.Universities
{
    public class UniversityEducationVM
    {
        public Guid guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<EducationVM> Educations { get; set; }
    }

}

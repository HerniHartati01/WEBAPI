using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Bookings;
using WEBAPI.ViewModels.Educations;
using WEBAPI.ViewModels.Others;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : BaseController <Education, EducationVM>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _educationMapper;

        public EducationController(IEducationRepository educationRepository,
            IMapper<Education, EducationVM> educationMapper) : base (educationRepository, educationMapper)
        {
            _educationRepository = educationRepository;
            _educationMapper = educationMapper;
        }


    }
}

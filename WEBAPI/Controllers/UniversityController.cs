using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Educations;
using WEBAPI.ViewModels.Universities;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<University, UniversityVM> _universityMapper;
        private readonly IMapper<Education, EducationVM> _educationMapper;
        public UniversityController(IUniversityRepository universityRepository, 
            IEducationRepository educationRepository, 
            IMapper<University, UniversityVM> universityMapper,
            IMapper<Education, EducationVM> educationMapper)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _universityMapper = universityMapper;
            _educationMapper = educationMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound();
            }

            var data = universities.Select(_universityMapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid:guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return NotFound();
            }

            var data = _universityMapper.Map(university);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var universityConverted = _universityMapper.Map(universityVM);
            var result = _universityRepository.Create(universityConverted);
            if (result is null)
            {
                return BadRequest();
            }
            
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(UniversityVM universityVM)
        {
            var universityConverted = _universityMapper.Map(universityVM);
            var isUpdated = _universityRepository.Update(universityConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpGet("ByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var university = _universityRepository.GetByName(name);
            if (!university.Any())
            {
                return NotFound();
            }
            var data = university.Select(_universityMapper.Map);
            return Ok(data);
        }

        [HttpGet("WithEducation")]
        public IActionResult GetAllWithEducation()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound();
            }

            var results = new List<UniversityEducationVM>();
            foreach (var university in universities)
            {
                var education = _educationRepository.GetByUniversityId(university.Guid);
                var educationMapped = education.Select(EducationVM.ToVM);

                var result = new UniversityEducationVM
                {
                    guid = university.Guid,
                    Code = university.Code,
                    Name = university.Name,
                    Educations = educationMapped
                };

                results.Add(result);
            }
            return Ok(results);
        }


    }

}


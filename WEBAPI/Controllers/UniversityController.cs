using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Educations;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Rooms;
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
                return NotFound(new ResponseVM<List<UniversityVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = universities.Select(_universityMapper.Map).ToList();
            return Ok(new ResponseVM<List<UniversityVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = data
            });
        }

        [HttpGet("{guid:guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return NotFound(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = _universityMapper.Map(university);
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var universityConverted = _universityMapper.Map(universityVM);
            var result = _universityRepository.Create(universityConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }
            
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpPut]
        public IActionResult Update(UniversityVM universityVM)
        {
            var universityConverted = _universityMapper.Map(universityVM);
            var isUpdated = _universityRepository.Update(universityConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }
            
            return Ok(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpGet("ByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var university = _universityRepository.GetByName(name);
            if (!university.Any())
            {
                return NotFound(new ResponseVM<UniversityVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found"
                });
            }
            var data = university.Select(_universityMapper.Map);
            return Ok(new ResponseVM<IEnumerable<UniversityVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = data
            });
        }

        [HttpGet("WithEducation")]
        public IActionResult GetAllWithEducation()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound(new ResponseVM<List<UniversityVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
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
            return Ok(new ResponseVM<IEnumerable<UniversityEducationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = results
            });
        }


    }

}


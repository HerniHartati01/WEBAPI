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
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _educationMapper;

        public EducationController(IEducationRepository educationRepository,
            IMapper<Education, EducationVM> educationMapper)
        {
            _educationRepository = educationRepository;
            _educationMapper = educationMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound(new ResponseVM<List<EducationVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"

                });
            }

            var data = educations.Select(_educationMapper.Map).ToList();
            return Ok(new ResponseVM<List<EducationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var educations = _educationRepository.GetByGuid(guid);
            if (educations is null)
            {
                return NotFound(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"

                });
            }
            var data = _educationMapper.Map(educations);
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            var educationConverted = _educationMapper.Map(educationVM);
            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var educationConverted = _educationMapper.Map(educationVM);
            var isUpdated = _educationRepository.Update(educationConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

    }
}

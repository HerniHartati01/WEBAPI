using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IRepositoryGeneric<Education> _educationRepository;

        public EducationController(IRepositoryGeneric<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound();
            }

            return Ok(educations);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var educations = _educationRepository.GetByGuid(guid);
            if (educations is null)
            {
                return NotFound();
            }

            return Ok(educations);
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            var result = _educationRepository.Create(education);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Education education)
        {
            var isUpdated = _educationRepository.Update(education);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}

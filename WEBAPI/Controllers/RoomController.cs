using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.ViewModels.Rooms;
using WEBAPI.ViewModels.Universities;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _roomMapper;

        public RoomController(IRoomRepository roomRepository, 
            IMapper<Room, RoomVM> roomMapper)
        {
            _roomRepository = roomRepository;
            _roomMapper = roomMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }
            var data = rooms.Select(_roomMapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid:guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var rooms = _roomRepository.GetByGuid(guid);
            if (rooms is null)
            {
                return NotFound();
            }
            var data = _roomMapper.Map(rooms);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var roomConverted = _roomMapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var roomConverted = _roomMapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(roomConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("ByName/{name}")]
        public IActionResult GetByName(String name)
        {
            var rooms = _roomRepository.GetByName(name);
            if (!rooms.Any())
            {
                return NotFound();
            }
            var data = rooms.Select(_roomMapper.Map);
            return Ok(data);
        }

    }

}

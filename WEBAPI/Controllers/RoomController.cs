using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Roles;
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
                return NotFound(new ResponseVM<List<RoomVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = rooms.Select(_roomMapper.Map).ToList();
            return Ok(new ResponseVM<List<RoomVM>>
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
            var rooms = _roomRepository.GetByGuid(guid);
            if (rooms is null)
            {
                return NotFound(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
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
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
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
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok();
        }

        [HttpGet("ByName/{name}")]
        public IActionResult GetByName(String name)
        {
            var rooms = _roomRepository.GetByName(name);
            if (!rooms.Any())
            {
                return NotFound(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = rooms.Select(_roomMapper.Map);
            return Ok(data);
        }

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(room);
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(room);
        }

        private string GetRoomStatus(Booking booking, DateTime dateTime)
        {

            if (booking.StartDate <= dateTime && booking.EndDate >= dateTime)
            {
                return "Occupied";
            }
            else if (booking.StartDate > dateTime)
            {
                return "Booked";
            }
            else
            {
                return "Available";
            }
        }

    }

}

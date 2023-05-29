using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Roles;
using WEBAPI.ViewModels.Rooms;
using WEBAPI.ViewModels.Universities;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController <Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _roomMapper;

        public RoomController(IRoomRepository roomRepository, 
            IMapper<Room, RoomVM> roomMapper) : base (roomRepository, roomMapper)
        {
            _roomRepository = roomRepository;
            _roomMapper = roomMapper;
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
            return Ok(new ResponseVM<IEnumerable<RoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
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

            return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = room
            });
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

            return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = room
            });
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

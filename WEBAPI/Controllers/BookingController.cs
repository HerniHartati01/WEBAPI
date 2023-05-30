using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.Utility;
using WEBAPI.ViewModels.AccountRoles;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Bookings;
using WEBAPI.ViewModels.Others;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController <Booking, BookingVM>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _bookingMapper;
      
        public BookingController(IBookingRepository bookingRepository,
            IMapper<Booking, BookingVM> bookingMapper) : base (bookingRepository, bookingMapper)
        {
            _bookingRepository = bookingRepository;
            _bookingMapper = bookingMapper;
           
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {

                var results = _bookingRepository.GetAllBookingDetail();

                return Ok( results);
            }
            catch
            {
                return Ok(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Ada Error",
                });
            }

        }
        
        [HttpGet("BookingDetail/{guid}")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var bookingDetailVM = _bookingRepository.GetBookingDetailByGuid(guid);

                if (bookingDetailVM is null)
                {
                    return Ok(new ResponseVM<BookingVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Tidak ditemukan objek dengan Guid ini",
                    });
                }


                return Ok(bookingDetailVM);
            }
            catch
            {
                return Ok(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Ada Error",
                });
            }
        }

        [HttpGet("bookinglength")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = bookingLengths
            });
        }


    }
}

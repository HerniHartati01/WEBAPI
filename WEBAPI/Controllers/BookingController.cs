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
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _bookingMapper;
      
        public BookingController(IBookingRepository bookingRepository,
            IMapper<Booking, BookingVM> bookingMapper)
        {
            _bookingRepository = bookingRepository;
            _bookingMapper = bookingMapper;
           
        }

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var booking = _bookingRepository.GetAll();
            if (!booking.Any())
            {
                return NotFound(new ResponseVM<List<BookingVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"

                });
            }

            var data = booking.Select(_bookingMapper.Map).ToList();
            return Ok(new ResponseVM<List<BookingVM>>
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
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"

                });
            }
            var data = _bookingMapper.Map(booking); 
            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);
            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);
            var isUpdated = _bookingRepository.Update(bookingConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
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

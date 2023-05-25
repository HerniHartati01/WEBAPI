using WEBAPI.Models;
using WEBAPI.ViewModels.Bookings;

namespace WEBAPI.Contracts
{
    public interface IBookingRepository : IRepositoryGeneric<Booking>
    {
        IEnumerable<BookingDurationVM> GetBookingDuration();
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
        IEnumerable<BookingDetailVM> GetAllBookingDetail();
    }
}

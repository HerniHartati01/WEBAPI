using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class BookingRepository : RepositoryGeneric<Booking>, IBookingRepository
    {
        public BookingRepository(BookingMangementDbContext context) : base(context) { }

        
    }
}

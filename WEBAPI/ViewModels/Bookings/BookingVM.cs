using System.ComponentModel.DataAnnotations.Schema;
using WEBAPI.Utility;

namespace WEBAPI.ViewModels.Bookings
{
    public class BookingVM
    {
        public Guid? Guid { get; set; }
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public StatusLevel Status { get; set; }
       
        public string Remarks { get; set; }
    }
}

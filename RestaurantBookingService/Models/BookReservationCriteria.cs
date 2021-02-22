using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingService.Models
{
    public class BookReservationCriteria
    {
        public long RestaurantId { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public List<long> UserIds { get; set; }
    }
}

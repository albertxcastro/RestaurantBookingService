using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class ReservationToUser
    {
        public long? ReservationId { get; set; }
        public long? UserId { get; set; }

        //public virtual Reservation Reservation { get; set; }
        //public virtual User ReservationNavigation { get; set; }
    }
}

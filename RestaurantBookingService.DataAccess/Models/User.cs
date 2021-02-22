using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class User
    {
        //public User()
        //{
        //    Diners = new HashSet<Diner>();
        //}

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ReservationId { get; set; }

        //public virtual Reservation Reservation { get; set; }
        //public virtual ICollection<Diner> Diners { get; set; }
    }
}

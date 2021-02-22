using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class Table
    {
        //public Table()
        //{
        //    Reservations = new HashSet<Reservation>();
        //}

        public long Id { get; set; }
        public int Capacity { get; set; }
        public long RestaurantId { get; set; }

        //public virtual Restaurant Restaurant { get; set; }
        //public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

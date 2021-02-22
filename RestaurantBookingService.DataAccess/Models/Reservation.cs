using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class Reservation
    {
        //public Reservation()
        //{
        //    Users = new HashSet<User>();
        //}

        public long Id { get; set; }
        public DateTime? DateTime { get; set; }
        public bool MarkForDelete { get; set; }
        public long TableId { get; set; }
        public long RestaurantId { get; set; }

        //public virtual Restaurant Restaurant { get; set; }
        //public virtual Table Table { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}

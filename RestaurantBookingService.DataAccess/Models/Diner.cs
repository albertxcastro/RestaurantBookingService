using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class Diner
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? RestaurantId { get; set; }

        //public virtual Restaurant Restaurant { get; set; }
        //public virtual User User { get; set; }
    }
}

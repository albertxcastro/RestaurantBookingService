using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class DietaryRestriction
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

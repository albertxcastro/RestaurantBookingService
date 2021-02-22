using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class RestaurantToDietaryRestriction
    {
        public long RestaurantId { get; set; }
        public long DietaryRestrictionId { get; set; }

        //public virtual DietaryRestriction DietaryRestriction { get; set; }
        //public virtual Restaurant Restaurant { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class UserToDietaryRestriction
    {
        public long? UserId { get; set; }
        public long? DietaryRestrictionId { get; set; }

        //public virtual DietaryRestriction DietaryRestriction { get; set; }
        //public virtual User User { get; set; }
    }
}

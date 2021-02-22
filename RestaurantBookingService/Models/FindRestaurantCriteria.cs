using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingService.Models
{
    public class FindRestaurantCriteria
    {
        public List<long> UserIds { get; set; }
        public DateTime AvailabilityDateTime { get; set; }
    }
}

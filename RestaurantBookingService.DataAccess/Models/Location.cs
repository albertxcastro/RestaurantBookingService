using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class Location
    {
        //public Location()
        //{
        //    Restaurants = new HashSet<Restaurant>();
        //}

        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        //public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

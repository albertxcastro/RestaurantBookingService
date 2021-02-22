using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RestaurantBookingService.DataAccess.Models
{
    public partial class Restaurant
    {
        //public Restaurant()
        //{
        //    Diners = new HashSet<Diner>();
        //    Reservations = new HashSet<Reservation>();
        //    Tables = new HashSet<Table>();
        //}

        public long Id { get; set; }
        public string Name { get; set; }
        public long? LocationId { get; set; }

        [NotMapped]
        public virtual Location Location { get; set; }
    //    public virtual ICollection<Diner> Diners { get; set; }
    //    public virtual ICollection<Reservation> Reservations { get; set; }
    //    public virtual ICollection<Table> Tables { get; set; }
    }
}

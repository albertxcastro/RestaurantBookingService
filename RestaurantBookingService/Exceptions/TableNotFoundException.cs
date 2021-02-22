using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingService.Exceptions
{
    public class TableNotFoundException : Exception
    {
        public TableNotFoundException()
        {
        }

        public TableNotFoundException(string message)
            : base(message)
        {
        }

        public TableNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

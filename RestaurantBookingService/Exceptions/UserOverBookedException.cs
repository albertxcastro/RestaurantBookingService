using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingService.Exceptions
{
    public class UserOverBookedException : Exception
    {
        public UserOverBookedException()
        {
        }

        public UserOverBookedException(string message)
            : base(message)
        {
        }

        public UserOverBookedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

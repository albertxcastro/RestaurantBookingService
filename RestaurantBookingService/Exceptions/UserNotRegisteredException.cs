using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingService.Exceptions
{
    public class UserNotRegisteredException : Exception
    {
        public UserNotRegisteredException()
        {
        }

        public UserNotRegisteredException(string message)
            : base(message)
        {
        }

        public UserNotRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RestaurantBookingService.DataAccess.Context;
using RestaurantBookingService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers
{
    public class BaseManager
    {
        protected Context _dbContext;

        public void SetContext(Context dbContext)
        {
            _dbContext = dbContext;
        }

        protected async Task<bool> CheckRegisteredUsersAsync(List<long> userIds, CancellationToken cancellationToken)
        {
            var requiredSeats = userIds.Count;
            var registeredUsers = await _dbContext.Users.Where(user => userIds.Contains(user.Id)).ToListAsync(cancellationToken);

            //This means that not all the usersIds are registered, and we need them all to be registered to know their dietary restrictions
            if (registeredUsers.Count != requiredSeats)
            {
                throw new UserNotRegisteredException("One or more Users are not registered in the system.");
            }

            return true;
        }
    }
}

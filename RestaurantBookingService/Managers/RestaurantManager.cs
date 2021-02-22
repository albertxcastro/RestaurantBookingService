using Microsoft.EntityFrameworkCore;
using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Managers.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers
{
    public class RestaurantManager : BaseManager, IRestaurantManager
    {
        public async Task<List<Restaurant>> GetAsync(FindRestaurantCriteria criteria, CancellationToken cancellationToken)
        {
            await CheckRegisteredUsersAsync(criteria.UserIds, cancellationToken);

            var availableRestaurants = _dbContext.GetRestaurantByUsersDietaryRestriction(criteria.UserIds);

            // Now that we have all the restaurants that match the users dietary restrictions, we need to see if there are available tables
            // with the requested number of seats

            // now let's check if the table is available or not
            var availableTables = _dbContext.GetAvailableTablesByRestaurantIds(availableRestaurants.Select(r => r.Id).ToList(), criteria.UserIds.Count);

            if (availableTables != null && !availableTables.Any())
            {
                throw new TableNotFoundException("There are no available restaurants/tables for the selected users.");
            }

            // There are available tables!
            var restaurants = await _dbContext.Restaurants
                .Where(rest => availableTables.Select(table => table.RestaurantId).Contains(rest.Id))
                .ToListAsync(cancellationToken);

            foreach (var restaurant in restaurants)
            {
                var location = await _dbContext.Locations.Where(loc => loc.Id == restaurant.LocationId).FirstAsync(cancellationToken);
                restaurant.Location = new Location
                {
                    Id = location.Id,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
            }

            return restaurants;
        }
    }
}

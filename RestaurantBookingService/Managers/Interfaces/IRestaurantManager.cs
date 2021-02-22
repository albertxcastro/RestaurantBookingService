using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers.Interfaces
{
    public interface IRestaurantManager
    {
        public Task<List<Restaurant>> GetAsync(FindRestaurantCriteria criteria, CancellationToken cancelationToken);
    }
}

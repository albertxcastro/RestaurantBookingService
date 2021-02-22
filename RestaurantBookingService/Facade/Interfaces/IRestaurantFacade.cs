using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Facade.Interfaces
{
    public interface IRestaurantFacade
    {
        public Task<List<Restaurant>> GetAsync(FindRestaurantCriteria criteria, CancellationToken cancellationToken);
    }
}

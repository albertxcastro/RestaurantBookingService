using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Managers;
using RestaurantBookingService.Managers.Interfaces;
using RestaurantBookingService.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Facade
{
    public class RestaurantFacade : IRestaurantFacade
    {
        private readonly IRestaurantManager _restaurantManager;
        private readonly IRepository _repository;

        public RestaurantFacade(IRepository repository)
        {
            _repository = repository;
            _restaurantManager = _repository.GetManagerInstance<RestaurantManager>();
        }

        public async Task<List<Restaurant>> GetAsync(FindRestaurantCriteria criteria, CancellationToken cancellationToken)
        {
            return await _restaurantManager.GetAsync(criteria, cancellationToken);
        }
    }
}

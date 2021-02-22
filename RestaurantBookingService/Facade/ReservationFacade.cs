using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Managers;
using RestaurantBookingService.Managers.Interfaces;
using RestaurantBookingService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Facade
{
    public class ReservationFacade : IReservationFacade
    {
        private readonly IReservationManager _reservationManager;
        private readonly IRepository _repository;

        public ReservationFacade(IRepository repository)
        {
            _repository = repository;
            _reservationManager = _repository.GetManagerInstance<Managers.ReservationManager>();
        }
        public ReservationFacade(IReservationManager reservationManager)
        {
            _reservationManager = reservationManager;
        }

        public async Task<string> BookReservationAsync(BookReservationCriteria criteria, CancellationToken cancellationToken)
        {
            return await _reservationManager.BookReservationAsync(criteria, cancellationToken);
        }

        public async Task<string> DeleteReservationAsync(long reservationId, CancellationToken cancellationToken)
        {
            return await _reservationManager.DeleteReservationAsync(reservationId, cancellationToken);
        }
    }
}

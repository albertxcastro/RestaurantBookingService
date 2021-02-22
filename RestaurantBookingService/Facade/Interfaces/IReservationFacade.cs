using RestaurantBookingService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Facade.Interfaces
{
    public interface IReservationFacade
    {
        public Task<string> BookReservationAsync(BookReservationCriteria criteria, CancellationToken cancellationToken);
        public Task<string> DeleteReservationAsync(long reservationId, CancellationToken cancellationToken);
    }
}

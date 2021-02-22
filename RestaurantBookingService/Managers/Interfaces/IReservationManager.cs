using RestaurantBookingService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers.Interfaces
{
    public interface IReservationManager
    {
        public Task<string> BookReservationAsync(BookReservationCriteria criteria, CancellationToken cancellationToken);
        public Task<string> DeleteReservationAsync(long reservationId, CancellationToken cancellationToken);
    }
}

using Microsoft.AspNetCore.Mvc;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationFacade _reservationFacade;

        public ReservationController(IReservationFacade reservationFacade)
        {
            _reservationFacade = reservationFacade;
        }

        [HttpPost("BookReservation")]
        public async Task<ActionResult> GetRestaurantAsync([FromBody] BookReservationCriteria criteria, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _reservationFacade.BookReservationAsync(criteria, cancellationToken);
                return Ok(message);
            }
            catch (UserNotRegisteredException ex)
            {
                return Content(ex.Message);
            }
            catch (UserOverBookedException ex)
            {
                return Content(ex.Message);
            }
            catch (TableNotFoundException ex)
            {
                return Content(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("DeleteReservation/{reservationId}")]
        public async Task<ActionResult> DeleteAsync(long reservationId, CancellationToken cancellationToken)
        {
            try
            {
                await _reservationFacade.DeleteReservationAsync(reservationId, cancellationToken);
            }
            catch (ReservationNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok("Correctly Deleted");
        }
    }
}

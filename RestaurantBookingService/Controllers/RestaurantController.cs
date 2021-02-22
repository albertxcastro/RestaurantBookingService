using Microsoft.AspNetCore.Mvc;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Facade;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Controllers
{
    /// <summary>
    /// Controller responsible for finding restaurants
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantFacade _restaurantFacade;

        public RestaurantController(IRestaurantFacade restaurantFacade)
        {
            _restaurantFacade = restaurantFacade;
        }

        [HttpGet("Find")]
        public async Task<ActionResult> GetRestaurantAsync([FromBody] FindRestaurantCriteria criteria, CancellationToken cancellationToken)
        {
            try
            {
                var resturants = await _restaurantFacade.GetAsync(criteria, cancellationToken);
                return Ok(resturants);
            }
            catch (UserNotRegisteredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserOverBookedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TableNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
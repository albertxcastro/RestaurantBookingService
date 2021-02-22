using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantBookingService.Controllers;
using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantBookingService.UnitTests.ControllerTests
{
    public class RestaurantControllerTests : ControllerTestBase
    {
        [Fact]
        public void FindAsync_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var restaurantList = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Location = new Location { Latitude = 0.0, Longitude = 0.0, Id = 1 },
                    LocationId = 1,
                    Name = "La casa de Toño"
                }
            };

            var criteria = new FindRestaurantCriteria
            {
                AvailabilityDateTime = DateTime.Now,
                UserIds = new List<long> { 1, 2 }
            };

            var mockFacade = new Mock<IRestaurantFacade>();
            mockFacade.Setup(facade => facade.GetAsync(It.IsAny<FindRestaurantCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(restaurantList);
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var okResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void FindAsync_Test_WhenUserNotRegisteredExceptionIsThrown_ExpectBadRequest()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new FindRestaurantCriteria
            {
                AvailabilityDateTime = DateTime.Now,
                UserIds = new List<long> { 1, 2 }
            };

            var mockFacade = new Mock<IRestaurantFacade>();
            mockFacade.Setup(facade => facade.GetAsync(It.IsAny<FindRestaurantCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new UserNotRegisteredException("Ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var badRequestResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void FindAsync_Test_WhenUserOverBookedExceptionIsThrown_ExpectBadRequest()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new FindRestaurantCriteria
            {
                AvailabilityDateTime = DateTime.Now,
                UserIds = new List<long> { 1, 2 }
            };

            var mockFacade = new Mock<IRestaurantFacade>();
            mockFacade.Setup(facade => facade.GetAsync(It.IsAny<FindRestaurantCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new UserOverBookedException("Ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var badRequestResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void FindAsync_Test_WhenTableNotFoundExceptionIsThrown_ExpectBadRequest()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new FindRestaurantCriteria
            {
                AvailabilityDateTime = DateTime.Now,
                UserIds = new List<long> { 1, 2 }
            };

            var mockFacade = new Mock<IRestaurantFacade>();
            mockFacade.Setup(facade => facade.GetAsync(It.IsAny<FindRestaurantCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new TableNotFoundException("Ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var badRequestResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void FindAsync_Test_WhenExceptionIsThrown_ExpectStatusCode500()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new FindRestaurantCriteria
            {
                AvailabilityDateTime = DateTime.Now,
                UserIds = new List<long> { 1, 2 }
            };

            var mockFacade = new Mock<IRestaurantFacade>();
            mockFacade.Setup(facade => facade.GetAsync(It.IsAny<FindRestaurantCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var statusCode500 = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<ObjectResult>(statusCode500.Result);
        }

        private RestaurantController GetControllerInstance(IRestaurantFacade facadeObject, Dictionary<string, string> headers = default)
        {
            var context = GetHttpContext(headers);
            var controllerContext = new ControllerContext { HttpContext = context };
            return new RestaurantController(facadeObject) { ControllerContext = controllerContext };
        }
    }
}

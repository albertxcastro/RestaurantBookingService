using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantBookingService.Controllers;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RestaurantBookingService.UnitTests.ControllerTests
{
    public class ReservationControllerTests : ControllerTestBase
    {
        [Fact]
        public void BookReservationAsync_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new BookReservationCriteria
            {
                ReservationDateTime = DateTime.Now,
                RestaurantId = 1,
                UserIds = new List<long> { 1, 2 }
            };
            var message = string.Empty;
            var mockFacade = new Mock<IReservationFacade>();
            mockFacade.Setup(facade => facade.BookReservationAsync(It.IsAny<BookReservationCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(message);
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var okResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void BookReservationAsync_WhenUserNotRegisteredException_ContentIsExpected_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new BookReservationCriteria
            {
                ReservationDateTime = DateTime.Now,
                RestaurantId = 1,
                UserIds = new List<long> { 1, 2 }
            };
            var message = string.Empty;
            var mockFacade = new Mock<IReservationFacade>();
            mockFacade.Setup(facade => facade.BookReservationAsync(It.IsAny<BookReservationCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new UserNotRegisteredException("ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var contentResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<ContentResult>(contentResult.Result);
        }

        [Fact]
        public void BookReservationAsync_WhenUserOverBookedException_ContentIsExpected_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new BookReservationCriteria
            {
                ReservationDateTime = DateTime.Now,
                RestaurantId = 1,
                UserIds = new List<long> { 1, 2 }
            };
            var message = string.Empty;
            var mockFacade = new Mock<IReservationFacade>();
            mockFacade.Setup(facade => facade.BookReservationAsync(It.IsAny<BookReservationCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new UserOverBookedException("ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var contentResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<ContentResult>(contentResult.Result);
        }

        [Fact]
        public void BookReservationAsync_WhenTableNotFoundException_ContentIsExpected_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new BookReservationCriteria
            {
                ReservationDateTime = DateTime.Now,
                RestaurantId = 1,
                UserIds = new List<long> { 1, 2 }
            };
            var message = string.Empty;
            var mockFacade = new Mock<IReservationFacade>();
            mockFacade.Setup(facade => facade.BookReservationAsync(It.IsAny<BookReservationCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new TableNotFoundException("ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var contentResult = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<ContentResult>(contentResult.Result);
        }

        [Fact]
        public void BookReservationAsync_WhenException_ContentIsExpected_Test()
        {
            // Arrange
            var header = new Dictionary<string, string>
            {
                { "UserId", "1" }
            };

            var criteria = new BookReservationCriteria
            {
                ReservationDateTime = DateTime.Now,
                RestaurantId = 1,
                UserIds = new List<long> { 1, 2 }
            };
            var message = string.Empty;
            var mockFacade = new Mock<IReservationFacade>();
            mockFacade.Setup(facade => facade.BookReservationAsync(It.IsAny<BookReservationCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("ex"));
            var controller = GetControllerInstance(mockFacade.Object, header);

            // Act
            var statusCode = controller.GetRestaurantAsync(criteria, CancellationToken.None);

            // Assert
            Assert.IsType<ObjectResult>(statusCode.Result);
        }

        private ReservationController GetControllerInstance(IReservationFacade facadeObject, Dictionary<string, string> headers = default)
        {
            var context = GetHttpContext(headers);
            var controllerContext = new ControllerContext { HttpContext = context };
            return new ReservationController(facadeObject) { ControllerContext = controllerContext };
        }
    }
}

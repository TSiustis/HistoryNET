using History.Api.Controllers;
using History.Api.Data;
using History.Api.Services;
using History.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace History.Tests
{
    public class EventControllerTest
    {

        [Fact]
        public void EventGetById_ReturnsHttpNotFound_ForInvalidId()
        {
            //Arrange
            int testSessionId = 12311;
            var testObject = new Event();
            var testList = new List<Event>() { testObject };
            var dbSetMock = new Mock<DbSet<Event>>();
            dbSetMock.As<IQueryable<Event>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Event>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Event>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Event>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            var context = new Mock<HistoryDbContext>();
            context.Setup(x => x.Set<Event>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericRepository<Event>(context.Object);
            var controller = new EventsController(context.Object);

            // Act
            var result = controller.GetEventById(testSessionId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(testSessionId, notFoundObjectResult.Value);
        }
        //[Fact]
        //public void ForSession_ReturnsIdeasForSession()
        //{
        //    var controller = new EventsController(new HistoryDbContext());

        //    var item = GetTestEvents()[0];

        //    var result = controller.GetEventById(2) as StatusCodeResult;
        //    Assert.NotNull(result);
        //    Assert.IsType<StatusCodeResult>(result);
        //    Assert.Equal(HttpStatusCode.NoContent.ToString(), result.StatusCode.ToString());
        //}
        private List<Event> GetTestEvents()
        {
            var events = new List<Event>();
            events.Add(new Event()
            {
                Id = 1,
                Content = "Test One"
            });
            events.Add(new Event()
            {
                Id = 2,
                Content = "Test Two"
            });
            return events;
        }
    }
}

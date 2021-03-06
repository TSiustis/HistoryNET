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

            var optionsBuilder = new DbContextOptionsBuilder<HistoryDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var _dbContext = new HistoryDbContext(optionsBuilder.Options);

            var controller = new EventsController(_dbContext);

            // Act
            var result =  controller.GetEventById(1,"");
            Assert.IsType<NotFoundObjectResult>(result);
          
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

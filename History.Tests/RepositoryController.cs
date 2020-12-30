using History.Api.Data;
using History.Api.Services;
using History.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace History.Tests
{
    public class RepositoryController
    {
        [Fact]
        public void Insert_TestClassObjectPassed_()
        {
            // Arrange
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
            repository.Insert(testObject);

            //Assert
            context.Verify(x => x.Set<Event>());
            dbSetMock.Verify(x => x.Add(It.Is<Event>(y => y == testObject)));
        }
       
    }
}

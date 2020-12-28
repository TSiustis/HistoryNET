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
    public class UnitTest1
    {
        [Fact]
        public void Insert_TestClassObjectPassed_()
        {
            // Arrange
            var testObject = new Birth();
            var testList = new List<Birth>() { testObject };
            var dbSetMock = new Mock<DbSet<Birth>>();
            dbSetMock.As<IQueryable<Birth>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Birth>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Birth>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Birth>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            var context = new Mock<HistoryDbContext>();
            context.Setup(x => x.Set<Birth>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericRepository<Birth>(context.Object);
            repository.Insert(testObject);

            //Assert
            context.Verify(x => x.Set<Birth>());
            dbSetMock.Verify(x => x.Add(It.Is<Birth>(y => y == testObject)));
        }
       
    }
}

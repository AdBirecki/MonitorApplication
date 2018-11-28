using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Commands.Handler;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_DAL.Interfaces;
using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Contexts;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace MonitorApplication_BL_Tests
{
    [TestFixture]
    public class RegisterUserTests
    {
        ILoggerFactory _loggerFactory;
        [SetUp]
        public void SpecSetUp()
        {
            _loggerFactory = new LoggerFactory();

        }

        [Test]
        public void RegisterUser()
        {
            #region Users mock data
            User expectedUser_1 = new User() {
                Username = "TestUser1",
                Password = "TestPassword1",
                Name = "A",
                Surname = "B"
            };
            User expectedUser_2 = new User()
            {
                Username = "TestUser2",
                Password = "TestPassword2",
                Name = "A",
                Surname = "B"
            };
            User expectedUser_3 = new User()
            {
                Username = "TestUser3",
                Password = "TestPassword3",
                Name = "A",
                Surname = "B"
            };
            #endregion

            RegisterUserCommand registerUserCommand = new RegisterUserCommand()
            {
                UserName = "TestUser1",
                Password = "TestPassword",
                Name = "A",
                Surname = "B"
            };
            // DbSet
            var mockUsersData = new User[] { expectedUser_1, expectedUser_2, expectedUser_3 }.AsQueryable();
            DbSet<User> mockSet = Substitute.For<DbSet<User>,IQueryable<User>>();

            // create mock for DbSet
            ((IQueryable<User>)mockSet).Provider.Returns(mockUsersData.Provider);
            ((IQueryable<User>)mockSet).Expression.Returns(mockUsersData.Expression);
            ((IQueryable<User>)mockSet).ElementType.Returns(mockUsersData.ElementType);
            ((IQueryable<User>)mockSet).GetEnumerator().Returns(mockUsersData.GetEnumerator());

            var mockOrdersDbContex = Substitute.For<OrdersDbContext>();
            mockOrdersDbContex.Users.Returns(mockSet);

            RegisterUserHandler registerUserHandler = new RegisterUserHandler(_loggerFactory, mockOrdersDbContex);
            Assert.Throws<ArgumentException>(() => { registerUserHandler.Execute(registerUserCommand);});
        }

    }
}

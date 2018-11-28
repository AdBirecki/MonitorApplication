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
    /* Unit tests were added for commands.  CQRS separates operations that occur in system into  two groups Commands and Queries. 
     * Commands and are responsible for changes in the system and as such are most likely to cause errors. */
    [TestFixture]
    public class RegisterUserTests
    {
        ILoggerFactory _loggerFactory;
        OrdersDbContext _ordersDbContext;
        User expectedUser_1;
        User expectedUser_2;
        User expectedUser_3;

        [SetUp]
        public void SpecSetUp()
        {
            _loggerFactory = new LoggerFactory();
            #region Users mock data
            expectedUser_1 = new User()
            {
                Username = "TestUser1",
                Password = "TestPassword1",
                Name = "A",
                Surname = "B"
            };
            expectedUser_2 = new User()
            {
                Username = "TestUser2",
                Password = "TestPassword2",
                Name = "A",
                Surname = "B"
            };
            expectedUser_3 = new User()
            {
                Username = "TestUser3",
                Password = "TestPassword3",
                Name = "A",
                Surname = "B"
            };

            // DbSet
            var mockUsersData = new User[] { expectedUser_1, expectedUser_2, expectedUser_3 }.AsQueryable();
            DbSet<User> mockSet = Substitute.For<DbSet<User>, IQueryable<User>>();

            // create mock for DbSet
            ((IQueryable<User>)mockSet).Provider.Returns(mockUsersData.Provider);
            ((IQueryable<User>)mockSet).Expression.Returns(mockUsersData.Expression);
            ((IQueryable<User>)mockSet).ElementType.Returns(mockUsersData.ElementType);
            ((IQueryable<User>)mockSet).GetEnumerator().Returns(mockUsersData.GetEnumerator());

            _ordersDbContext = Substitute.For<OrdersDbContext>();
            _ordersDbContext.Users.Returns(mockSet);
            #endregion

        }

        [Test]
        public void RegisterInvalidPassword()
        {
          RegisterUserCommand registerUserCommand = new RegisterUserCommand()
          {
                UserName = "TestUser4",
                Password = "",
                Name = "Name",
                Surname = "Surname"
          };

           bool isValid = registerUserCommand.IsValid();
           Assert.IsFalse(isValid);
           Assert.IsEmpty(registerUserCommand.Password);
        }

        [Test]
        public void RegisterInvalidUserName()
        {
            RegisterUserCommand registerUserCommand = new RegisterUserCommand()
            {
                UserName = "",
                Password = "",
                Name = "Name",
                Surname = "Surname"
            };

            bool isValid = registerUserCommand.IsValid();
            Assert.IsFalse(isValid);
            Assert.IsEmpty(registerUserCommand.UserName);
            Assert.IsEmpty(registerUserCommand.Password);
        }

        [Test]
        public void RegisterUserException()
        {
            RegisterUserCommand registerUserCommand = new RegisterUserCommand()
            {
                UserName = "TestUser1",
                Password = "TestPassword",
                Name = "A",
                Surname = "B"
            };

            RegisterUserHandler registerUserHandler = new RegisterUserHandler(_loggerFactory, _ordersDbContext);
            Assert.IsNotEmpty(registerUserCommand.UserName);
            Assert.Throws<ArgumentException>(() => { registerUserHandler.Execute(registerUserCommand);});
        }

    }
}

using Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly DbSet<User> _usersDbSetMock;
        private readonly Mock<ICrudAPIContext> _contextMock;

        public UserControllerTests()
        {
            this._usersDbSetMock = Substitute.For<DbSet<User>, IQueryable<User>>();     // NSubstitute > mock the DBSet as Queryable
            this._contextMock = new Mock<ICrudAPIContext>();                            // Moq > mock the dbSet Users registers

            var usersMock = MockInfraConfigs.CreateDbSetMock(GetFakeListUsers());
            this._contextMock.Setup(x => x.Users).Returns(usersMock.Object);
            this._contextMock.Object.Users = _usersDbSetMock;
        }

        private IEnumerable<User> GetFakeListUsers()
        {
            var users = new List<User>
        {
                new User(){ Id = 1, Name = "foo", Email = "foo@gmail.com", Password = "123"},
                new User(){ Id = 5, Name = "foo2", Email = "foo2@gmail.com", Password = "321"},
                new User(){ Id = 9, Name = "foo3", Email = "foo3@gmail.com", Password = "312"}
        };

            return users;
        }

        [TestMethod]
        public void RegisterUser_PassingSaveChangesReturning1_ReturnsSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(1);

            var userFake = new User() { Id = 99, Name = "Test Name", Email = "testName@gmail.com", Password = "123" };
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.RegisterUser(userFake);


            //Assert
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void RegisterUser_PassingSaveChangesReturning0_ReturnsNoSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(0);

            var userFake = new User() { Id = 99, Name = "Test Name", Email = "testName@gmail.com", Password = "123" };
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.RegisterUser(userFake);


            //Assert
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void GetUserById_PassingValidId_ReturnsNotNullAndSuccess()
        {
            // Arrange
            var userIdFake = 5;
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.GetUserById(userIdFake);


            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void GetUserById_PassingInvalidId_ReturnsNullAndNoSuccess()
        {
            // Arrange
            var userIdFake = -9999;
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.GetUserById(userIdFake);


            //Assert
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [TestMethod]
        public void GetAllUsers_PassingValidUserList_ReturnsThreeResultsOnListAndSuccess()
        {
            // Arrange
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.GetAllUsers();


            //Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Results.Count(), 3);
        }

        [TestMethod]
        public void GetAllUsers_PassingInvalidUserList_ReturnsZeroResultsOnListAndNoSuccess()
        {
            // Arrange
            var userEmptyFakeList = new List<User>() { };
        
            var usersMock = MockInfraConfigs.CreateDbSetMock(userEmptyFakeList);
            this._contextMock.Setup(x => x.Users).Returns(usersMock.Object);
        
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);
        

            // Act
            var response = userControllerInstance.GetAllUsers();


            //Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.Results.Count(), 0);
        }

        [TestMethod]
        public void EditUser_PassingSaveChangesReturning1_ReturnsNotNullAndSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(1);

            var userFake = new User() { Id = 99, Name = "Test Edit Name", Email = "testEditName@gmail.com", Password = "edit123" };
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.EditUser(userFake);


            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void EditUser_PassingSaveChangesReturning0_ReturnsNullAndNoSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(0);

            var userFake = new User() { Id = 99, Name = "Test Edit Name", Email = "testEditName@gmail.com", Password = "edit123" };
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.EditUser(userFake);


            //Assert
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [TestMethod]
        public void DeleteUser_PassingValidIdAndSaveChangesReturning1_ReturnsSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(1);

            var userIdFake = 1;
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.DeleteUser(userIdFake);


            //Assert
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void DeleteUser_PassingInvalidId_ReturnsUserNotFoundAndNoSuccess()
        {
            // Arrange
            var userIdFake = -999;
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.DeleteUser(userIdFake);


            //Assert
            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.Message.Contains("not found"));
        }

        [TestMethod]
        public void DeleteUser_PassingValidIdAndSaveChangesReturning0_ReturnsNoSuccess()
        {
            // Arrange
            _contextMock.Setup(x => x.SaveChanges()).Returns(0);

            var userIdFake = 1;
            var userServiceInstance = new UserService(_contextMock.Object);
            var userControllerInstance = new UserController(userServiceInstance);


            // Act
            var response = userControllerInstance.DeleteUser(userIdFake);


            //Assert
            Assert.IsFalse(response.Success);
        }
    }
}
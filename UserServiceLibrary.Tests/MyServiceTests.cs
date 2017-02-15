using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Exceptions;

namespace MyServiceLibrary.Tests
{
    [TestClass]
    public class MyServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            // arrange  
            var s = new UserService();
            // act 
            s.Add(null);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof (ExistingUserException))]
        public void Add_ExistingUser_ExceptionThrown()
        {
            // arrange  
            var s = new UserService();
            var user = new User {DateOfBirth = DateTime.Today, FirstName = "Peter", LastName = "Parker"};
            // act 
            s.Add(user);
            s.Add(user);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void Add_User_Success()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User {DateOfBirth = DateTime.Today, FirstName = "Peter", LastName = "Parker"};
            // act 
            s.Add(user);
            // assert
            var actual = s.SearchByLastName(user.LastName).FirstOrDefault();
            Assert.AreEqual(user.FirstName, actual.FirstName);

            s.Delete(actual);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Delete_NullUser_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            // act 
            s.Delete(null);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof (NotExistingUserException))]
        public void Delete_UnexistingUser_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User {DateOfBirth = DateTime.Today, FirstName = "Peter", LastName = "Parker", Id = 123};
            // act 
            s.Delete(user);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof (NotValidUserException))]
        public void Delete_NotValidUser_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User();
            // act 
            s.Delete(user);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User {DateOfBirth = DateTime.Today, FirstName = "Peter", LastName = "Parker"};
            s.Add(user);
            user = s.SearchByLastName(user.LastName).FirstOrDefault();
            // act 
            s.Delete(user);
            // assert is handled by ExpectedException
            var actual = s.SearchByLastName(user.LastName).FirstOrDefault();
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchByFirstName_NullFirstName_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            // act 
            s.SearchByFirstName(null);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void SearchByFirstName_EmptyFirstName_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            // act 
            s.SearchByFirstName(string.Empty);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void SearchByFirstName_FirstName_Success()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User {LastName = "LN", FirstName = "FN"};
            s.Add(user);
            user = s.SearchByFirstName(user.FirstName).FirstOrDefault();
            // act 
            s.Delete(user);
            // assert is handled by ExpectedException
            Assert.IsNull(s.SearchByFirstName(user.FirstName).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchByLastName_NullLastName_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            // act 
            s.SearchByLastName(null);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void SearchByLastName_EmptyLastName_ExceptionThrown()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            // act 
            s.SearchByLastName(string.Empty);
            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void SearchByLastName_LastName_Success()
        {
            // arrange  
            var s = new UserService(o => o.GetHashCode());
            var user = new User {LastName = "LN", FirstName = "FN"};
            s.Add(user);
            user = s.SearchByLastName(user.LastName).FirstOrDefault();
            // act 
            s.Delete(user);
            // assert is handled by ExpectedException
            Assert.IsNull(s.SearchByLastName(user.LastName).FirstOrDefault());
        }
    }
}
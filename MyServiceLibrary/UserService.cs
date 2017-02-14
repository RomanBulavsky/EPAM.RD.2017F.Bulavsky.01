using System;
using System.Collections.Generic;
using System.Linq;
using MyServiceLibrary.Exceptions;

namespace MyServiceLibrary
{
    /// <summary>
    /// Class that provide work with storage of <see cref="User"/>
    /// </summary>
    /// <seealso cref="User"/>
    /// <seealso cref="ExistingUserException"/>
    /// <seealso cref="NotExistingUserException"/>
    /// <seealso cref="NotValidUserException"/>
    public class UserService
    {
        public UserService()
        {
            this.IdCreator = o => o.GetHashCode();
            this.Storage = new HashSet<User>();
        }

        /// <summary>
        /// The constructor takes <see cref="Func{T,TResult}"/> for creating <see cref="User"/> <see cref="User.Id"/>
        /// </summary>
        /// <param name="idCreator"><see cref="Func{T,TResult}"/> where T is <see cref="object"/>, TResult is <see cref="int"/></param>
        public UserService(Func<object, int> idCreator)
        {
            this.IdCreator = idCreator;
            this.Storage = new HashSet<User>();
        }

        private HashSet<User> Storage { get; }

        private Func<object, int> IdCreator { get; }

        /// <summary>
        /// Adds new <see cref="User"/> into storage
        /// </summary>
        /// <param name="user"> <see cref="User"/> instance </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ExistingUserException"></exception>
        public void Add(User user)
        {
            CheckArguments((object)user);
            if (this.Storage.Contains(user))
            {
                throw new ExistingUserException();
            }

            user.Id = this.IdCreator(user);
            this.Storage.Add(user);
        }

        /// <summary>
        /// Removes <see cref="User"/> from storage
        /// </summary>
        /// <param name="user"> <see cref="User"/> instance </param>
        /// /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotExistingUserException"></exception>
        /// <exception cref="NotValidUserException"></exception>
        public void Delete(User user)
        {
            CheckArguments(user);
            if (!this.Storage.Remove(user))
            {
                throw new NotExistingUserException();
            }
        }

        /// <summary>
        /// Searches <see cref="User"/> entities by <see cref="firstname"/>
        /// </summary>
        /// <param name="firstname"> <see cref="string"/></param>
        /// <returns> <see cref="IEnumerable{User}"/> that consist the <see cref="firstname"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<User> SearchByFirstName(string firstname)
        {
            CheckArguments(firstname);
            return this.Storage.Where(u => u.FirstName.Equals(firstname));
        }

        /// <summary>
        /// Searches <see cref="User"/> entities by <see cref="lastName"/>
        /// </summary>
        /// <param name="lastName"> <see cref="string"/></param>
        /// <returns> <see cref="IEnumerable{User}"/> that consist the <see cref="lastName"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            CheckArguments(lastName);
            return this.Storage.Where(u => u.LastName.Equals(lastName));
        }

        /// <summary>
        /// Checks argument for null and, if argument is <see cref="string"/> for nonempty
        /// </summary>
        /// <param name="o"><see cref="object"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static void CheckArguments(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException();
            }

            if (o.ToString().Equals(string.Empty))
            {
                throw new ArgumentException("Empty String");
            }
        }

        /// <summary>
        /// Checks argument(<see cref="User"/>) for null and for validity
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static void CheckArguments(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(); 
            }

            if (user.Id == null || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                throw new NotValidUserException();
            }             
        }
    }
}
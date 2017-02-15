using System;

namespace MyServiceLibrary
{
    public class User
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        // Override GetHC and Eq
    }
}

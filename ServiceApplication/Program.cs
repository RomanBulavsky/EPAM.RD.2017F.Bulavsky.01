using System;
using System.Linq;
using MyServiceLibrary;

namespace ServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new UserService();
            var key = '0';

            while (key != 'e')
            {
                switch (key)
                {
                    case 'a':
                        Console.WriteLine("Adding");
                        Console.WriteLine("Write Name");
                        var name = Console.ReadLine();
                        Console.WriteLine("Write LastName");
                        var lastName = Console.ReadLine();
                        service.Add(new User{FirstName = name, LastName = lastName});// Stupid StyleCop
                        break;
                    case 'd':
                        Console.WriteLine("Delete");
                        Console.WriteLine("Write LastName");
                        var lastName2 = Console.ReadLine();
                        var u = service.SearchByLastName(lastName2).FirstOrDefault();
                        service.Delete(u);
                        break;
                    case 'l':
                        Console.WriteLine("Last");
                        break;
                    case 'f':
                        Console.WriteLine("First");
                        break;
                    /*case 's':
                        Console.WriteLine("Show");
                        foreach (var user in service.Storage)
                        {
                            Console.WriteLine($"Name: {user.FirstName}, LastName: {user.LastName}");
                        }
                        break;*/
                    default:
                        Console.WriteLine("a - Add");
                        Console.WriteLine("d - Delete");
                        Console.WriteLine("s - Show");
                        Console.WriteLine("l - Search by last name");
                        Console.WriteLine("f - Search by first name");
                        break;
                }

                key = Console.ReadKey().KeyChar;
            }

            // 1. Add a new user to the storage.
            // 2. Remove an user from the storage.
            // 3. Search for an user by the first name.
            // 4. Search for an user by the last name.
        }
    }
}
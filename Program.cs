using Microsoft.EntityFrameworkCore;

namespace PhoneBook
{
    internal class PhoneBook
    {
        static void Main(string[] args)
        {
            using var db = new PhoneBookContext();

            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            var phoneNumber = Console.ReadLine();
            Console.Write("Enter E-mail: ");
            var eMail = Console.ReadLine();

            var contact = new Contact { Name = name, PhoneNumber = phoneNumber, EMail = eMail };
            db.Contacts.Add(contact);
            db.SaveChanges();

            var query = from c in db.Contacts
                        orderby c.Name
                        select c;

            Console.WriteLine("All Contacts");
            Console.WriteLine("\nName\tPhone Number\tE-mail");
            foreach (var item in query)
            {
                Console.Write($"{item.Name}\t{item.PhoneNumber}\t{item.EMail}\n");
            }

            Console.WriteLine("\n\nPress ENTER to exit");
            Console.ReadLine();
        }
    }
}

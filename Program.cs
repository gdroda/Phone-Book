using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;

namespace PhoneBook
{
    internal class PhoneBook
    {
        static readonly PhoneBookContext _db = new();
        static void Main(string[] args)
        {
            MainMenu();

        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("~ Phone Book ~\n\n");
                Console.WriteLine("1. Show Contacts");
                Console.WriteLine("2. Add New Contacts");
                Console.WriteLine("3. Delete Contacts");
                Console.WriteLine("4. Exit");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PrintContacts();
                        break;
                    case "2":
                        AddNewContact();
                        break;
                    case "3":
                        DeleteContacts();
                        break;
                    case "4":
                        return;
                    default:
                        continue;
                }
            }
        }

        static void AddNewContact()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter name: ");
                var name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name can't be empty, please try again.\nPress ENTER to return");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Phone number: ");
                string? phoneNumber = ValidatePhoneNumber(Console.ReadLine());
                if (phoneNumber == null) return;
                    
                Console.Write("Enter E-mail: ");
                var eMail = ValidateEmail(Console.ReadLine());
                if (eMail == null) return;

                var contact = new Contact { Name = name, PhoneNumber = phoneNumber, EMail = eMail };
                _db.Contacts.Add(contact);
                _db.SaveChanges();

                Console.WriteLine("\n\nPress ENTER to return");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error...: {ex}");
                throw;
            }
        }

        static string? ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                Console.WriteLine("\nPhone number can't be empty, please try again.\nPress ENTER to return");
                Console.ReadLine();
                return null;
            }
            else
            {
                var phoneCleaned = phoneNumber.Trim();
                bool isValid = phoneCleaned.Length == 10 && phoneCleaned.All(char.IsDigit);

                if (!isValid)
                {
                    Console.WriteLine("\nPhone number has to be 10 numeric digits only.\nPress Enter to return");
                    Console.ReadLine();
                    return null;
                }
                return phoneCleaned;
            }
        }

        static string? ValidateEmail(string eMail)
        {
            if (string.IsNullOrWhiteSpace(eMail))
            {
                Console.WriteLine("\nE-mail can't be empty, please try again.\nPress ENTER to return");
                Console.ReadLine();
                return null;
            }
            else
            {
                var eMailCleaned = eMail.Trim();
                bool isValid = eMailCleaned.Count(c => c == '@') == 1;

                if (!isValid)
                {
                    Console.WriteLine("\nWrong E-mail format.\nPress Enter to return");
                    Console.ReadLine();
                    return null;
                }
                return eMailCleaned;
            }
        }

        static void DeleteContacts()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter name to delete: ");
                string? input = Console.ReadLine();

                _db.Contacts.Where(p => p.Name == input).ExecuteDelete();
                _db.SaveChanges();

                Console.WriteLine("\nPress ENTER to return");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error...: {ex}");
                throw;
            }
        }

        static void PrintContacts()
        {
            Console.Clear();
            try
            {
                var query = from c in _db.Contacts
                            orderby c.Name
                            select c;

                Console.WriteLine("All Contacts");
                Console.WriteLine("\nName\tPhone Number\tE-mail");
                foreach (var item in query)
                {
                    Console.Write($"{item.Name}\t{item.PhoneNumber}\t{item.EMail}\n");
                }

                Console.WriteLine("\nPress ENTER to return");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error...: {ex}");
                throw;
            }
        }

    }
}

using Microsoft.EntityFrameworkCore;
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
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Send Test email");
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
                        return;
                    case "4":
                        SendMail();
                        break;
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
                Console.Write("Enter Phone Number: ");
                var phoneNumber = Console.ReadLine();
                Console.Write("Enter E-mail: ");
                var eMail = Console.ReadLine();

                var contact = new Contact { Name = name, PhoneNumber = phoneNumber, EMail = eMail };
                _db.Contacts.Add(contact);
                _db.SaveChangesAsync();

                Console.WriteLine("\n\nPress ENTER to return");
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

        static void SendMail()
        {
            string? addr = "";
            string? pw = "";

            string? mailFrom = "";
            string? mailTo = "";

            try
            {
                MailMessage mail = new();

                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = "Test email!";
                mail.Body = "Hello, this is a test!";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465); // gmail doesnt work anymore with less secure apps, to be changed.
                smtp.Credentials = new NetworkCredential($"{addr}", $"{pw}");
                smtp.EnableSsl = true;

                smtp.Send(mail);
                Console.WriteLine("\nE-mail sent successfully");
                Console.WriteLine("\nPress ENTER to return");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}

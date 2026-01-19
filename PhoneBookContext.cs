using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook
{
    public class PhoneBookContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Phonebook;Trusted_Connection=True;Integrated security=SSPI;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact { ContactId = 1, Name = "George", PhoneNumber = "1234567890", EMail = "seededmail@mail.com"}
                );
        }
    }

    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
    }
}

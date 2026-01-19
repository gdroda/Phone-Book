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

        /*
        public PhoneBookContext()
        {
            // @"Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;Integrated security=SSPI);   ConnectRetryCount=0";

        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Phonebook;Trusted_Connection=True;Integrated security=SSPI;ConnectRetryCount=0");
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

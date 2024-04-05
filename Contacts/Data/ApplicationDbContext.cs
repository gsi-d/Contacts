﻿using Contacts.Dados;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public DbSet<Contact> Contact { get; set; }

    }
}
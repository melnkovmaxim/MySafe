using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySafe.Tests.Ef.Models;
using RestSharp.Validation;

namespace MySafe.Tests.Ef
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<SecureStorageEntity> SecureStorage { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}

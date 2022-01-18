using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class MyAppContext : IdentityDbContext<ApplicationUser>
    {
        public MyAppContext(DbContextOptions options) : base(options) {}

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }

    public class MyAppDbContextFactory : IDesignTimeDbContextFactory<MyAppContext>
    {

        MyAppContext IDesignTimeDbContextFactory<MyAppContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MyAppContext>();
            var connectionString = configuration.GetConnectionString("MyAppConnection");

            //builder.UseSqlServer(connectionString);
            builder.UseSqlServer(connectionString);

            return new MyAppContext(builder.Options);
        }
    }
}

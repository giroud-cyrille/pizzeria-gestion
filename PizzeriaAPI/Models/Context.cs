using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaAPI.Models;
using ProjetcLibrary.Models;

namespace PizzeriaAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<ProductOption> Options { get; set; }
        public DbSet<Produit> Products { get; set; }
        public DbSet<User> Users
        {
            get;
            set;
        }
    }
}

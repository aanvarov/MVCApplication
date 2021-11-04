using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCWebApplication.Models;

namespace MVCWebApplication.Data
{
    public class MVCWebApplicationContext : DbContext
    {
        public MVCWebApplicationContext (DbContextOptions<MVCWebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<MVCWebApplication.Models.Product> Product { get; set; }
    }
}

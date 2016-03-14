using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Service2TheRescue.Models
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Customer> Customers  { get; set; }
    }
}
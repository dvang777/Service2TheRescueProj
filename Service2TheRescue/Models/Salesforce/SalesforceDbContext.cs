using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Service2TheRescue.Models.Salesforce
{
    public class SalesforceDbContext : DbContext
    {
        public SalesforceDbContext() : base("DefaultConnection")
        {

        }

        //public DbSet<UserInfo> Users { get; set; }
    }
}
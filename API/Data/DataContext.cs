using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xenergy.Entities;

namespace API.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserGroup> UserGroups{get; set;}

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<AccessRule> AccessRules { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroupAccessRule> UserGroupAccessRule { get; set; }
    }
}
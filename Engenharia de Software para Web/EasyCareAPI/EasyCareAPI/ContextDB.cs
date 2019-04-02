using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EasyCareAPI.Models;

namespace EasyCareAPI
{
    public class ContextDB : DbContext
    {
        public ContextDB() : base("EasyCare")
        {

        }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }*/
        public DbSet<UserModel> Users { get; set; }

    }

}
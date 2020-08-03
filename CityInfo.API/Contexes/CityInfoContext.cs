using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Contexes
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointOfInterests { get; set; }

        //Option2 - using datbaseOptions, configuration goes into configureservice()
        public CityInfoContext(DbContextOptions options) : base(options)
        {
            // While performing update-database, it will create an instance of context which will create a database, 
            // and in the migrations if we are trying to create tables, it will fail with below error:
            // There is already an object named 'Cities' in the database.
            //Database.EnsureCreated();
        }

        //Option1
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

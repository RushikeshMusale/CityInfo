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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(new City
                {
                    Id = 1,
                    Name = "Pune",
                    Description = "Center of education"
                },
                new City
                {
                    Id = 2,
                    Name = "Mumbai",
                    Description = "Financial capital",
                },
                new City
                {
                    Id = 3,
                    Name = "Delhi",
                    Description = "Capital of India"
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(new PointOfInterest
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Shanivar Wada",
                    Description = "Historical value"
                },
                new PointOfInterest
                {
                    Id = 2,
                    CityId = 1,
                    Name = "Tulshi Baug",
                    Description = "Market"
                },
                new PointOfInterest
                {
                    Id = 3,
                    CityId = 2,
                    Name = "Mumba Devi",
                    Description = "Temple"
                },
                new PointOfInterest
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Antilia",
                    Description = "Richest Man's House"
                },
                new PointOfInterest
                {
                    Id = 5,
                    CityId = 3,
                    Name = "Red Fort",
                    Description = "Historical value"
                }
                );

            base.OnModelCreating(modelBuilder);
        }


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

using CityInfo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; } = new CityDataStore(); // readonly property
        public List<CityDto>  Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    Id=1,
                    Name="Pune",
                    Description="Center of education",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id=1,
                            Name="Shanivar Wada",
                            Description ="Historical value"
                        },
                        new PointOfInterestDto
                        {
                            Id=2,
                            Name="Tulshi Baug",
                            Description ="Market"
                        }
                    }
                },
                new CityDto
                {
                    Id=2,
                    Name="Mumbai",
                    Description ="Financial capital",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id=3,
                            Name="Mumba Devi",
                            Description ="Temple"
                        },
                        new PointOfInterestDto
                        {
                            Id=4,
                            Name="Antilia",
                            Description ="Richest Man's House"
                        }
                    }
                },
                new CityDto
                {
                    Id=3,
                    Name= "Delhi",
                    Description = "Capital of India",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id=5,
                            Name="Red Fort",
                            Description ="Historical value"
                        }                       
                    }
                }
            };
        }
    }
}

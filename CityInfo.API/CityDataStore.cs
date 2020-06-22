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
                    Description="Center of education"
                },
                new CityDto
                {
                    Id=2,
                    Name="Mumbai",
                    Description ="Financial capital"
                },
                new CityDto
                {
                    Id=3,
                    Name= "Delhi",
                    Description = "Capital of India"
                }
            };
        }
    }
}

using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithoutPointOfInterests>();
            CreateMap<City, CityDto>();            
        }
    }
}

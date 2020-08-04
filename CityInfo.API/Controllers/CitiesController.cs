using CityInfo.API.Entities;
using CityInfo.API.Model;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController] // This improves api development experience
    // If this is not applied, and route is not configured, we will get 404 error
    // when we apply the attribute, and route is not configured, it will say route not configured

    //[Route("api/[Controller]")] // third way of routing
    [Route("api/cities")] // Preffered way of routing 
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        //[HttpGet("api/cities")] This is one way of routing
        //[Route("api/cities")] second way of routing
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityInfoRepository.GetCities();
            var results = new List<CityWithoutPointOfInterests>();
            foreach (var city in cities)
            {
                results.Add(new CityWithoutPointOfInterests
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                });
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterests = false) 
        {
            var city = _cityInfoRepository.GetCity(id, includePointOfInterests);

            if (city == null)
                return NotFound();

            if(includePointOfInterests)
            {
                var cityResult = new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach (var pointOfInterest in city.PointOfInterests)
                {
                    cityResult.PointOfInterests.Add(new PointOfInterestDto
                    {
                        Id=  pointOfInterest.Id,
                        Name = pointOfInterest.Name,
                        Description = pointOfInterest.Description
                    });
                }

                return Ok(cityResult);
            }

            var cityWithoutPointOfInterestsResult = new CityWithoutPointOfInterests
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
            };

            return Ok(cityWithoutPointOfInterestsResult);                       
        }
    }
}

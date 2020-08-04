using AutoMapper;
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
        private readonly IMapper _mapper;

        //[HttpGet("api/cities")] This is one way of routing
        //[Route("api/cities")] second way of routing
        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityInfoRepository.GetCities();
            
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterests>>(cities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterests = false) 
        {
            var city = _cityInfoRepository.GetCity(id, includePointOfInterests);

            if (city == null)
                return NotFound();

            if(includePointOfInterests)                          
                return Ok(_mapper.Map<CityDto>(city));            

            return Ok(_mapper.Map<CityWithoutPointOfInterests>(city));                       
        }
    }
}

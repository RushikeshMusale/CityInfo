using CityInfo.API.Contexes;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointOfInterests)
        {
            if (includePointOfInterests)
                return _context.Cities
                    .Include(c => c.PointOfInterests)
                    .Where(c => c.Id == cityId)
                    .FirstOrDefault();

            return _context.Cities
                .Where(c => c.Id == cityId)
                .FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForACity(int cityId, int pointOfInterestId)
        {
            return _context.PointOfInterests
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                .FirstOrDefault();   
        }

        public IEnumerable<PointOfInterest> GetPointOfInterestsForACity(int cityId)
        {
            return _context.PointOfInterests
                .Where(p => p.CityId == cityId)
                .ToList() ;
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterest(int cityId, PointOfInterest pointOfInterestfinal)
        {
            var city = GetCity(cityId, false);
            city.PointOfInterests.Add(pointOfInterestfinal);            
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }

        // In our case, we are using EntityFramework for persistance which tracks the updated entities
        // Some framework may not track it, so a general repository should have Update method
        public void Update(int cityId, PointOfInterest pointOfInterest)
        {
            
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }
    }
}

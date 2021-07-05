namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RentHome.Data;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.City;

    public class CityService : ICityService
    {
        private readonly IRepository<City> cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityListServiceModel>> AllCitiesAsync()
        {
            var cities = await this.cityRepository.All()
                .Select(c => new CityListServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId,
                })
                .ToListAsync();

            return cities;
        }

        public async Task<IEnumerable<CityListServiceModel>> AllCitiesByCountryAsync(int id)
        {
            var cities = await this.cityRepository.All()
                .Where(c => c.CountryId == id)
                .Select(c => new CityListServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId,
                })
                .ToListAsync();

            return cities;
        }
    }
}

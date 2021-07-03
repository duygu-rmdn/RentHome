namespace RentHome.Services.Data
{
    using System.Linq;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Home;

    public class GetCountService : IGetCountService
    {
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<City> cityRepository;

        public GetCountService(
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository)
        {
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                CountriesCount = this.countryRepository.All().Count(),
                CitiesCount = this.cityRepository.All().Count(),
            };

            return data;
        }
    }
}

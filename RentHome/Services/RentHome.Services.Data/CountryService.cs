namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Country;

    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> countryRepsitory;

        public CountryService(IRepository<Country> countryRepsitory)
        {
            this.countryRepsitory = countryRepsitory;
        }

        public async Task<IEnumerable<CountryListServiceModel>> AllCountriesAsync()
            => await this.countryRepsitory.All()
                .Select(c => new CountryListServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
    }
}

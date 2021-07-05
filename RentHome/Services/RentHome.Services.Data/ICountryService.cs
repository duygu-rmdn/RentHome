namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Country;

    public interface ICountryService
    {
        Task<IEnumerable<CountryListServiceModel>> AllCountriesAsync();
    }
}

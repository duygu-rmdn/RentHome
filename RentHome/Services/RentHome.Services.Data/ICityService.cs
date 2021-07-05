namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.City;

    public interface ICityService
    {
        Task<IEnumerable<CityListServiceModel>> AllCitiesAsync();

        Task<IEnumerable<CityListServiceModel>> AllCitiesByCountryAsync(int id);
    }
}

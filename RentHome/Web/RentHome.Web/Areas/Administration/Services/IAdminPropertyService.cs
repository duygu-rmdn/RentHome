namespace RentHome.Web.Areas.Administration.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Administration.Location;
    using RentHome.Web.ViewModels.Properties;

    public interface IAdminPropertyService
    {
        IEnumerable<PropertiesInListViewModel> ShowNewProperties();

        Task ChangeVisility(string id);

        Task AddCityWithCountry(LocationIndexFormModel input);
    }
}

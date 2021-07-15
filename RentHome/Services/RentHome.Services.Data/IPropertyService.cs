namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Properties;

    public interface IPropertyService
    {
        Task CreateAsync(CreatePropertyInputModel input, string userId, string imagePath);

        IEnumerable<PropertiesInListViewModel> GetAll(int page, int itemsPerPage = 12);
    }
}

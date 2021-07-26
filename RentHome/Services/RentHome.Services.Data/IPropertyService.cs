namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Properties;

    public interface IPropertyService
    {
        Task CreateAsync(CreatePropertyInputModel input, string userId, string imagePath);

        IEnumerable<PropertiesInListViewModel> GetAll(int page, int itemsPerPage = 12);

        IEnumerable<PropertiesInListViewModel> GetRandom(int count);

        int GetCount();

        SinglePropertyViewModel GetSingleProperty(string id);

        Task UpdateAsync(string id, EditPropertyInputModel input);

        EditPropertyInputModel GetById(string id);
    }
}

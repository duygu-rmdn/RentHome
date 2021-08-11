namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Search;

    public interface IPropertyService
    {
        Task CreateAsync(CreatePropertyInputModel input, string userId, string imagePath);

        IEnumerable<PropertiesInListViewModel> GetAll(int page, int itemsPerPage = 12);

        IEnumerable<PropertiesInListViewModel> GetRandom(int count);

        int GetCount();

        SinglePropertyViewModel GetSingleProperty(string id);

        Task UpdateAsync(string id, EditPropertyInputModel input);

        EditPropertyInputModel GetById(string id);

        PropertyStatus GetStatusById(string id);

        Task DeleteAsync(string id);

        IEnumerable<PropertiesInListViewModel> Search(SearchListInputModel input);
    }
}

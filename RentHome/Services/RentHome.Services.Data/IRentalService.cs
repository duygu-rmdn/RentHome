namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;

    public interface IRentalService
    {
        PropertiesInListViewModel GetProperty(string id);

        Task RequestAsync(RequestInputModel input, string userId, string id);
    }
}

namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Properties;

    public interface IPropertyService
    {
        Task CreateAsync(CreatePropertyInputModel input, string userId);
    }
}

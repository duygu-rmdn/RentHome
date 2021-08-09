namespace RentHome.Services.Data
{
    using System.Collections.Generic;

    using RentHome.Web.ViewModels.Properties;

    public interface IMyPropertiesService
    {
        IEnumerable<PropertiesInListViewModel> GetMyProperties(string id);
    }
}

namespace RentHome.Web.Areas.Administration.Services
{
    using System.Collections.Generic;

    using RentHome.Web.ViewModels.Properties;

    public interface IAdminPropertyService
    {
        IEnumerable<PropertiesInListViewModel> ShowNewProperties();

        void ChangeVisibility();
    }
}

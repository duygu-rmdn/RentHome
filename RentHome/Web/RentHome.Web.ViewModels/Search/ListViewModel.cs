namespace RentHome.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using RentHome.Web.ViewModels.Properties;

    public class ListViewModel
    {
        public IEnumerable<PropertiesInListViewModel> Properties { get; set; }
    }
}

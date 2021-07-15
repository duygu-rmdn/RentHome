namespace RentHome.Web.ViewModels.Properties
{
    using System.Collections.Generic;

    public class PropertiesListViewModel
    {
        public IEnumerable<PropertiesInListViewModel> Properties { get; set; }

        public int PageNumber { get; set; }
    }
}

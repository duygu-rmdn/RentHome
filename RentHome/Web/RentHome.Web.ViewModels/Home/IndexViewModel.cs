namespace RentHome.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using RentHome.Web.ViewModels.Properties;

    public class IndexViewModel
    {
        public IEnumerable<PropertiesInListViewModel> RandomProperties { get; set; }

        public int CountriesCount { get; set; }

        public int CitiesCount { get; set; }
    }
}

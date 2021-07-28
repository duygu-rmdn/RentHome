namespace RentHome.Web.ViewModels.Search
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.City;
    using RentHome.Web.ViewModels.Country;

    using static RentHome.Common.GlobalConstants;

    public class SearchIndexViewModel : BaseSearchModel
    {
        public IEnumerable<CityListServiceModel> CityList { get; set; }

        public IEnumerable<CountryListServiceModel> CountryList { get; set; }
    }
}

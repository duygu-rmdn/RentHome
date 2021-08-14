namespace RentHome.Web.Tests.Data
{
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Search;

    public class SearchData
    {
        public static SearchListInputModel SearchListInputModel()
            => new SearchListInputModel
            {
                Category = PropertyCategory.House,
                Status = PropertyStatus.ToRent,
                CityId = 1,
                CountryId = 1,
                MaxPrice = 10000,
                MinPrice = 100,
            };
    }
}

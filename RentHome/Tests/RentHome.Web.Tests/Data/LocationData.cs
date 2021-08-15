namespace RentHome.Web.Tests.Data
{
    using RentHome.Web.ViewModels.Administration.Location;

    public class LocationData
    {
        public static LocationIndexFormModel LocationIndexFormModel()
            => new LocationIndexFormModel
            {
                City = "City",
                Country = "Country",
                CountryCode = "CC",
            };
    }
}

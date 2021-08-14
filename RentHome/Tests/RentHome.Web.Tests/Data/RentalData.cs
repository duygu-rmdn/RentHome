namespace RentHome.Web.Tests.Data
{
    using System;

    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;

    public class RentalData
    {
        public static RequestInputModel RequestInputModel()
            => new RequestInputModel
            {
                Duration = 1,
                RentDate = DateTime.UtcNow,
                Status = PropertyStatus.ToRent,
                Message = "I want to rent it",
                Property = new PropertiesInListViewModel() { Id = "1" },
            };

        public static Request Request()
            => new Request
            {
                Id = "1",
                Duration = 1,
                RentDate = DateTime.UtcNow,
                PropertyId = "1",
                Status = RequestStatus.NA,
                Type = RequestType.ToRent,
            };
    }
}

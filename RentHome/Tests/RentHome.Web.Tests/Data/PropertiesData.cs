namespace RentHome.Web.Tests.Data
{
    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;

    public class PropertiesData
    {
        public static City City()
            => new City
            {
                Id = 1,
                CountryId = 1,
                Name = "dsdsdsdsds",
            };

        public static Country Country()
            => new Country
            {
                Name = "sdsdsdsd",
                Id = 1,
                Code = "1212",
            };

        public static CreatePropertyInputModel CreatePropertyInputModel()
            => new CreatePropertyInputModel
            {
                Name = "Name123",
                Category = PropertyCategory.House,
                Status = PropertyStatus.ToRent,
                Address = "12, Kamadahs, 2020",
                Description = "DescriptionDescriptionDescription",
                CityId = 1,
                CountryId = 1,
            };

        public static Property Property()
            => new Property
            {
                Id = "1",
                Description = "DescriptionDescription",
                Status = PropertyStatus.ToRent,
                Category = PropertyCategory.House,
                Name = "NameName",
                Price = 567,
            };

        public static SinglePropertyViewModel SinglePropertyViewModel()
            => new SinglePropertyViewModel
            {
                Id = "1",
                Description = "DescriptionDescription",
                StatusName = PropertyStatus.ToRent.ToString(),
                CaregoryName = PropertyCategory.House.ToString(),
                Name = "NameName",
                Price = 567,
            };

        public static EditPropertyInputModel EditPropertyInputModel()
            => new EditPropertyInputModel
            {
                Id = "1",
                Description = "DescriptionDescription",
                Status = PropertyStatus.ToRent,
                Category = PropertyCategory.House,
                Name = "NameName",
                Price = 567,
            };
    }
}

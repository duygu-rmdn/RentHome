namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Properties;

    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> propertyRepository;

        public PropertyService(IRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task CreateAsync(CreatePropertyInputModel input, string userId)
        {
            var property = new Property
            {
                Name = input.Name,
                Description = input.Description,
                Address = input.Address,
                Status = input.Status,
                Category = input.Category,
                Price = input.Price,
                CityId = input.CityId,
                OwnerId = userId,
            };

            await this.propertyRepository.AddAsync(property);
            await this.propertyRepository.SaveChangesAsync();
        }
    }
}

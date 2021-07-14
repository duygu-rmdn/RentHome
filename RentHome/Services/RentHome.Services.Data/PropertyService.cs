namespace RentHome.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Properties;

    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> propertyRepository;
        private readonly string[] allowdedExtensions = new[] { "jpg", "png", "gif" };

        public PropertyService(IRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task CreateAsync(CreatePropertyInputModel input, string userId, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/properties/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowdedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extention = extension,
                };
                property.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/properties/{dbImage.Id}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.propertyRepository.AddAsync(property);
            await this.propertyRepository.SaveChangesAsync();
        }
    }
}

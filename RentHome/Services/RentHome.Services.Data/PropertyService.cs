namespace RentHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Search;

    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> propertyRepository;
        private readonly IVotesService votesService;
        private readonly string[] allowdedExtensions = new[] { "jpg", "png", "gif" };

        public PropertyService(
            IRepository<Property> propertyRepository,
            IVotesService votesService)
        {
            this.propertyRepository = propertyRepository;
            this.votesService = votesService;
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

        public async Task DeleteAsync(string id)
        {
            var property = this.propertyRepository.All().FirstOrDefault(x => x.Id == id);
            property.IsDeleted = true;
            property.DeletedOn = DateTime.UtcNow;
            await this.propertyRepository.SaveChangesAsync();
        }

        public IEnumerable<PropertiesInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var properties = this.propertyRepository
                .AllAsNoTracking()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new PropertiesInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = $"{x.City.Name}, {x.City.Country.Name}",
                    CaregoryName = x.Category.ToString(),
                    Price = x.Price,
                    Description = x.Description.Substring(0, 70) + "...",
                    ImageUrl = "/images/properties/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                }).ToList();

            return properties;
        }

        public EditPropertyInputModel GetById(string id)
        {
            var property = this.propertyRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EditPropertyInputModel
                {
                    Id = id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    Status = x.Status,
                    Description = x.Description,
                }).FirstOrDefault();

            return property;
        }

        public int GetCount()
        {
            return this.propertyRepository.All().Count();
        }

        public IEnumerable<PropertiesInListViewModel> GetRandom(int count)
        {
            var properties = this.propertyRepository
                .All()
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .Select(x => new PropertiesInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = $"{x.City.Name}, {x.City.Country.Name}",
                    CaregoryName = x.Category.ToString(),
                    Price = x.Price,
                    Description = x.Description.Substring(0, 70) + "...",
                    ImageUrl = "/images/properties/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                }).ToList();

            return properties;
        }

        public SinglePropertyViewModel GetSingleProperty(string id)
        {
            var property = this.propertyRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SinglePropertyViewModel
                {
                    Id = id,
                    Name = x.Name,
                    Address = $"{x.Address}, {x.City.Name}, {x.City.Country.Name}",
                    CaregoryName = x.Category.ToString(),
                    Price = x.Price,
                    StatusName = x.Status.ToString(),
                    Description = x.Description,
                    OwnerEmail = x.Owner.Email,
                    OwnerFirstName = x.Owner.FirstName,
                    OwnerLastName = x.Owner.LastName,
                    OwnerUsername = x.Owner.UserName,
                    MenagerUsername = x.Manager.UserName,
                    ImageUrl = "/images/properties/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                    AverageVote = this.votesService.GetAverageVote(id),
                }).FirstOrDefault();

            return property;
        }

        public PropertyStatus GetStatusById(string id)
        {
            var propertyStatus = this.propertyRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.Status)
                .FirstOrDefault();

            return propertyStatus;
        }

        public IEnumerable<PropertiesInListViewModel> Search(SearchListInputModel input)
        {
            var result = this.propertyRepository.All()
                .Where(x => x.Price >= input.MinPrice
                    && x.Price <= input.MaxPrice
                    && x.Category == input.Category
                    && x.Status == input.Status
                    && x.CityId == input.CityId
                    && x.City.CountryId == input.CountryId
                    && x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new PropertiesInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = $"{x.City.Name}, {x.City.Country.Name}",
                    CaregoryName = x.Category.ToString(),
                    Price = x.Price,
                    Description = x.Description.Substring(0, 70) + "...",
                    ImageUrl = "/images/properties/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                }).ToList();

            return result;
        }

        public async Task UpdateAsync(string id, EditPropertyInputModel input)
        {
            var property = this.propertyRepository.All()
                .FirstOrDefault(x => x.Id == id);
            property.Name = input.Name;
            property.Description = input.Description;
            property.Price = input.Price;
            property.Status = input.Status;
            property.Category = input.Category;

            await this.propertyRepository.SaveChangesAsync();
        }
    }
}

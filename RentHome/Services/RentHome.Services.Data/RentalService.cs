namespace RentHome.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;

    public class RentalService : IRentalService
    {
        private readonly IRepository<Property> propertyRepository;
        private readonly IRepository<Request> requestRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public RentalService(
            IRepository<Property> propertyRepository,
            IRepository<Request> requestRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.propertyRepository = propertyRepository;
            this.requestRepository = requestRepository;
            this.userManager = userManager;
        }

        public PropertiesInListViewModel GetProperty(string id)
        {
            var property = this.propertyRepository
                .All()
                .Where(x => x.IsDeleted == false && x.Id == id)
                .Select(x => new PropertiesInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = $"{x.City.Name}, {x.City.Country.Name}",
                    CaregoryName = x.Category.ToString(),
                    Price = x.Price,
                    Description = x.Description.Substring(0, 70) + "...",
                    ImageUrl = "/images/properties/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extention,
                }).FirstOrDefault();

            return property;
        }

        public async Task RequestAsync(RequestInputModel input, string userId, string id)
        {
            var status = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.Status)
                .FirstOrDefault()
                == PropertyStatus.ToRent
                ? RequestType.ToRent : RequestType.ToManage;

            var request = new Request()
            {
                Type = status,
                Message = input.Message,
                ApplicationUserId = userId,
                PropertyId = id,
                Status = RequestStatus.NA,
            };

            await this.requestRepository.AddAsync(request);
            await this.requestRepository.SaveChangesAsync();
        }
    }
}

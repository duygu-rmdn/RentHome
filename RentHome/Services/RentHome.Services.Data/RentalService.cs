namespace RentHome.Services.Data
{
    using System;
    using System.Collections.Generic;
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

        public RentalService(
            IRepository<Property> propertyRepository,
            IRepository<Request> requestRepository)
        {
            this.propertyRepository = propertyRepository;
            this.requestRepository = requestRepository;
        }

        public async Task ApproveAsync(string id)
        {
            var property = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (property.Status == PropertyStatus.ToManage)
            {
                property.Status = PropertyStatus.Managed;
            }
            else if (property.Status == PropertyStatus.ToRent)
            {
                property.Status = PropertyStatus.Rented;
            }
            else
            {
                // TODO: throw new Exception(); => message
            }

            this.requestRepository.All()
                .Where(x => x.PropertyId == id)
                .FirstOrDefault()
                .Status = RequestStatus.Approved;

            await this.propertyRepository.SaveChangesAsync();

            // change request status -> view if...
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

        public IEnumerable<MyPropertyRequestsViewModel> MyPropertyRequests(string propertyId)
        {
            var requests = this.requestRepository.All()
                .Where(x => x.PropertyId == propertyId && x.Status != RequestStatus.Rejected)
                .Select(x => new MyPropertyRequestsViewModel
                {
                    Id = x.Id,
                    Username = x.ApplicationUser.UserName,
                    UserEmail = x.ApplicationUser.Email,
                    Type = x.Type,
                    Date = x.Date.ToString("dd/MM/yyyy"),
                    Message = x.Message,
                }).ToList();

            return requests;
        }

        public async Task RejectedAsync(string id)
        {
            this.requestRepository.All().Where(x => x.Id == id).FirstOrDefault().Status = RequestStatus.Rejected;

            await this.requestRepository.SaveChangesAsync();
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

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
        private readonly IRepository<Rental> rentalRepository;
        private readonly IRepository<Contract> contractRepository;

        public RentalService(
            IRepository<Property> propertyRepository,
            IRepository<Request> requestRepository,
            IRepository<Rental> rentalRepository,
            IRepository<Contract> contractRepository)
        {
            this.propertyRepository = propertyRepository;
            this.requestRepository = requestRepository;
            this.rentalRepository = rentalRepository;
            this.contractRepository = contractRepository;
        }

        public async Task ApproveAsync(string id, string requestId)
        {
            var property = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var request = this.requestRepository.All()
               .Where(x => x.Id == requestId)
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
                throw new Exception();
            }

            this.requestRepository.All()
                .Where(x => x.PropertyId == id)
                .FirstOrDefault()
                .Status = RequestStatus.Approved;

            var rental = new Rental()
            {
                PropertyId = id,
                RentDate = request.RentDate,
                Duration = request.Duration,
                TenantId = request.ApplicationUserId,
                Contract = new Contract
                {
                    Title = "Contract",
                    ManagerId = request.ApplicationUserId,
                },
            };

            property.ManagerId = request.ApplicationUserId;

            await this.rentalRepository.AddAsync(rental);

            await this.propertyRepository.SaveChangesAsync();
            await this.requestRepository.SaveChangesAsync();
            await this.contractRepository.SaveChangesAsync();
            await this.rentalRepository.SaveChangesAsync();
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
                    RentDate = x.RentDate.ToString("dd/MM/yyyy"),
                    Dutartion = x.Duration,
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
                RentDate = input.RentDate ?? DateTime.UtcNow,
                Duration = input.Duration,
            };

            await this.requestRepository.AddAsync(request);
            await this.requestRepository.SaveChangesAsync();
        }
    }
}

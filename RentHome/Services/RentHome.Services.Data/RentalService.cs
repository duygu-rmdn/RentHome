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
    using RentHome.Services.Messaging;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;

    using static RentHome.Common.GlobalConstants;

    public class RentalService : IRentalService
    {
        private readonly IRepository<Property> propertyRepository;
        private readonly IRepository<Request> requestRepository;
        private readonly IRepository<Rental> rentalRepository;
        private readonly IRepository<Contract> contractRepository;
        private readonly IEmailSenderService emailSenderService;

        public RentalService(
            IRepository<Property> propertyRepository,
            IRepository<Request> requestRepository,
            IRepository<Rental> rentalRepository,
            IRepository<Contract> contractRepository,
            IEmailSenderService emailSenderService)
        {
            this.propertyRepository = propertyRepository;
            this.requestRepository = requestRepository;
            this.rentalRepository = rentalRepository;
            this.contractRepository = contractRepository;
            this.emailSenderService = emailSenderService;
        }

        public async Task ApproveAsync(string propertyId, string requestId)
        {
            var property = this.propertyRepository.All()
                .Where(x => x.Id == propertyId)
                .FirstOrDefault();

            var request = this.requestRepository.All()
               .Where(x => x.Id == requestId)
               .FirstOrDefault();

            var userEmail = this.requestRepository.All()
               .Where(x => x.Id == requestId)
               .Select(x => x.ApplicationUser.Email)
               .FirstOrDefault();

            if (property.Status == PropertyStatus.ToManage)
            {
                property.Status = PropertyStatus.Managed;
                property.ManagerId = request.ApplicationUserId;
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
                .Where(x => x.Id == requestId)
                .FirstOrDefault()
                .Status = RequestStatus.Approved;

            var rental = new Rental()
            {
                PropertyId = propertyId,
                RentDate = request.RentDate,
                Duration = request.Duration,
                TenantId = request.ApplicationUserId,
                Contract = new Contract
                {
                    Title = "Contract",
                    ManagerId = request.ApplicationUserId,
                },
            };

            await this.rentalRepository.AddAsync(rental);

            await this.propertyRepository.SaveChangesAsync();
            await this.requestRepository.SaveChangesAsync();
            await this.contractRepository.SaveChangesAsync();
            await this.rentalRepository.SaveChangesAsync();

            var subject = "Approved request";
            var html = "Your request for " + property.Name + " is approved";

            this.emailSenderService.SendMail(SystemEmail, userEmail, subject, html);
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
                .Where(x => x.PropertyId == propertyId)
                .OrderBy(x => x.Status)
                .Select(x => new MyPropertyRequestsViewModel
                {
                    Id = x.Id,
                    Username = x.ApplicationUser.UserName,
                    UserEmail = x.ApplicationUser.Email,
                    Type = x.Type,
                    Status = x.Status,
                    RentDate = x.RentDate.ToString("dd/MM/yyyy"),
                    Dutartion = x.Duration,
                    Message = x.Message,
                }).ToList();

            return requests;
        }

        public async Task RejectedAsync(string id)
        {
            this.requestRepository.All().Where(x => x.Id == id).FirstOrDefault().Status = RequestStatus.Rejected;

            var property = this.requestRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.Property)
                .FirstOrDefault();

            var userEmail = this.requestRepository.All()
               .Where(x => x.Id == id)
               .Select(x => x.ApplicationUser.Email)
               .FirstOrDefault();

            var subject = "Rejected request";
            var html = "Your request for " + property.Name + " is rejected";

            this.emailSenderService.SendMail(SystemEmail, userEmail, subject, html);
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

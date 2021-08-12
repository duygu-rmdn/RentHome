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
        private readonly UserManager<ApplicationUser> userManager;

        public RentalService(
            IRepository<Property> propertyRepository,
            IRepository<Request> requestRepository,
            IRepository<Rental> rentalRepository,
            IRepository<Contract> contractRepository,
            IEmailSenderService emailSenderService,
            UserManager<ApplicationUser> userManager)
        {
            this.propertyRepository = propertyRepository;
            this.requestRepository = requestRepository;
            this.rentalRepository = rentalRepository;
            this.contractRepository = contractRepository;
            this.emailSenderService = emailSenderService;
            this.userManager = userManager;
        }

        public async Task ApproveAsync(string propertyId, string requestId)
        {
            var property = this.GetPropertyById(propertyId);

            var request = this.GetRequestById(requestId);

            var userEmail = this.GetUserEmail(requestId);

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

            this.GetRequestById(requestId)
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
                    ManagerId = property.ManagerId ?? property.OwnerId,
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

        public async Task<ContractViewModel> GetContractInfoAsync(string propertyId, string requestId)
        {
            var request = this.GetRequestById(requestId);

            var rental = this.rentalRepository.All()
                .Where(x => x.PropertyId == propertyId && request.PropertyId == propertyId)
                .FirstOrDefault();

            var property = this.GetPropertyById(propertyId);

            var contract = this.GetContractById(rental.ContractId);

            var owner = await this.userManager.FindByIdAsync(property.OwnerId);
            var tenant = await this.userManager.FindByIdAsync(rental.TenantId);

            var viewModel = new ContractViewModel()
            {
                Property = this.GetProperty(propertyId),
                Duration = request.Duration,
                RentDate = request.RentDate.ToString("dd/MM/yyyy"),
                Status = request.Status,
                OwnerUserName = owner.UserName,
                TenantUserName = tenant.UserName,
                Title = contract.Title,
            };

            return viewModel;
        }

        public PropertiesInListViewModel GetProperty(string id)
            => this.propertyRepository.All()
                .Where(x => x.IsDeleted == false && x.IsPublic == true && x.Id == id)
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

        public IEnumerable<MyPropertyRequestsViewModel> MyPropertyRequests(string propertyId)
            => this.requestRepository.All()
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

        public async Task RejectedAsync(string id)
        {
            this.GetRequestById(id)
                .Status = RequestStatus.Rejected;

            var property = this.requestRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.Property)
                .FirstOrDefault();

            var userEmail = this.GetUserEmail(id);

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

        public Request GetRequestById(string requestId)
            => this.requestRepository.All()
               .Where(x => x.Id == requestId)
               .FirstOrDefault();

        public Property GetPropertyById(string propertyId)
            => this.propertyRepository.All()
                .Where(x => x.Id == propertyId)
                .FirstOrDefault();

        public Contract GetContractById(string contractId)
            => this.contractRepository.All()
                .Where(x => x.Id == contractId)
                .FirstOrDefault();

        public string GetUserEmail(string id)
            => this.requestRepository.All()
               .Where(x => x.Id == id)
               .Select(x => x.ApplicationUser.Email)
               .FirstOrDefault();
    }
}

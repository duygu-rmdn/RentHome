namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Properties;

    public class MyPropertiesService : IMyPropertiesService
    {
        private readonly IRepository<Property> propertyRepository;

        public MyPropertiesService(IRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public IEnumerable<PropertiesInListViewModel> GetMyProperties(string id)
            => this.propertyRepository
                .AllAsNoTracking()
                .Where(x => x.IsDeleted == false && (x.OwnerId == id || x.ManagerId == id))
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
    }
}

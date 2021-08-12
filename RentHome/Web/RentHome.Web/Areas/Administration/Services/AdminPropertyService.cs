namespace RentHome.Web.Areas.Administration.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Properties;

    public class AdminPropertyService : IAdminPropertyService
    {
        private readonly IRepository<Property> propertyRepository;

        public AdminPropertyService(IRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task ChangeVisility(string id)
        {
            var property = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            property.IsPublic = !property.IsPublic;

            await this.propertyRepository.SaveChangesAsync();
        }

        public IEnumerable<PropertiesInListViewModel> ShowNewProperties()
            => this.propertyRepository.All()
                .Where(x => x.IsDeleted == false && x.IsPublic == false)
                .OrderBy(x => x.CreatedOn)
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

namespace RentHome.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RentHome.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria", Code = "BG" });
            await dbContext.Countries.AddAsync(new Country { Name = "Italy", Code = "IT" });
            await dbContext.Countries.AddAsync(new Country { Name = "Greece", Code = "GR" });
            await dbContext.Countries.AddAsync(new Country { Name = "Turkey", Code = "TR" });

            await dbContext.SaveChangesAsync();
        }
    }
}

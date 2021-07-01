using Database___exercise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Database___exercise
{
    public class ExerciseContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<CityCode> CityCodes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public ExerciseContext()
        {

        }
        public ExerciseContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ExerciseContext;Integrated Security=true;");
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
namespace carRental.DAL.Models.Context
{
    class CarContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}

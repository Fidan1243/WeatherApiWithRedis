using Microsoft.EntityFrameworkCore;
using WeatherApii.Entities;

namespace WeatherApii.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Weather> Weather { get; set; }
    }
}

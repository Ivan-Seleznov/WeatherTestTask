using Microsoft.EntityFrameworkCore;
using WeatherTestTask.Domain.Entities;

namespace WeatherTestTask.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeatherQueryEntity> WeatherQueries { get; set; }
}
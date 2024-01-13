using ArtistiqueCastingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<CastingModel> Casting { get; set; }
    public DbSet<CategoryModel> Category { get; set; }
    public DbSet<SubCategoryModel> SubCategory { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ArtistiqueLocal;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
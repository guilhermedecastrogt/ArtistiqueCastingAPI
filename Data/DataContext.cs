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
    public DbSet<AuthenticationModel> Authentication { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CastingModel>()
            .HasOne(c => c.SubCategory)
            .WithMany(s => s.Castings)
            .HasForeignKey(c => c.SubCategorySlug);

        modelBuilder.Entity<SubCategoryModel>()
            .HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategorySlug);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string? connectionString = Environment.GetEnvironmentVariable("ConnectionStringName");
            if (connectionString == null) connectionString = "Server=localhost,1433;Database=ArtistiqueLocal;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;"; 
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
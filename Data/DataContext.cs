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
    
    public DbSet<CastingSubCategoryModel> CastingSubCategory{ get; set; }

    public DbSet<SubCategoryCategoryModel> SubCategoryCategory { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CastingModel>()
            .HasMany(e => e.SubCategorys)
            .WithMany(e => e.Castings)
            .UsingEntity<CastingSubCategoryModel>(
                l => l.HasOne<SubCategoryModel>().WithMany().HasForeignKey(e => e.SubCategorySlug),
                r => r.HasOne<CastingModel>().WithMany().HasForeignKey(e => e.CastingId)
            );
        
        modelBuilder.Entity<SubCategoryModel>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.SubCategories)
            .UsingEntity<SubCategoryCategoryModel>(
                l => l.HasOne<CategoryModel>().WithMany().HasForeignKey(e => e.CategorySlug),
                r => r.HasOne<SubCategoryModel>().WithMany().HasForeignKey(e => e.SubCategorySlug)
            );
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
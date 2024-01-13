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

}
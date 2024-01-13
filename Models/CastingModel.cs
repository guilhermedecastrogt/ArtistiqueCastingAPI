using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class CastingModel
{
    public CastingModel()
    {
        Id = Guid.NewGuid();
    }
    
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string? CategorySlug { get; set; }
    public CategoryModel? Category { get; set; }
}
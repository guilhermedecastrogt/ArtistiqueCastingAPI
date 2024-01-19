using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class SubCategoryModel
{
    [Key]
    public string Slug { get; set; }
    public string Name { get; set; }

    public List<CastingModel>? Castings { get; set; }
    public List<CategoryModel>? Categories { get; set; }
}
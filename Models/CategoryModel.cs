using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class CategoryModel
{
    [Key]
    public string Slug { get; set; }
    public string Name { get; set; }
    public List<CastingModel>? Casting { get; set; }
    public List<SubCategoryModel>? SubCategorysGroup { get; set; }
}
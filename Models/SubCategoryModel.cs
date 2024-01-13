using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class SubCategoryModel
{
    [Key]
    public string Slug { get; set; }
    public string Name { get; set; }
    public string CategorySlug { get; set; }
    public CategoryModel? Category { get; set; }
}
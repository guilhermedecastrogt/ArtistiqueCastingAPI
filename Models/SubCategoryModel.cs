using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class SubCategoryModel
{
    [Key]
    public string Slug { get; set; }
    public string Name { get; set; }
    public string CategorySlug { get; set; }
    public virtual CategoryModel? Category { get; set; }
    public virtual ICollection<CastingModel>? Castings { get; set; }
}
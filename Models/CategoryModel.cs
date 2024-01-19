using System.ComponentModel.DataAnnotations;

namespace ArtistiqueCastingAPI.Models;

public class CategoryModel
{
    [Key]
    public string Slug { get; set; }
    public string Name { get; set; }

    public virtual ICollection<SubCategoryModel>? SubCategories { get; set; }
}
namespace ArtistiqueCastingAPI.Models;

public class UpdateSubCategoryModel
{
    public string slug { get; set; }
    public string name { get; set; }
    public string beforeSlug { get; set; }
    public string categorySlug { get; set; }
}
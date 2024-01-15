namespace ArtistiqueCastingAPI.Models;

public class UpdateCategoryRequestModel
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public string BeforeSlug { get; set; }
}

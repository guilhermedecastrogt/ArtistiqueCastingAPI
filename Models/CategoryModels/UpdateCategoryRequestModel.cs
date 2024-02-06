namespace ArtistiqueCastingAPI.Models;

public class UpdateCategoryRequestModel
{
    public string slug { get; set; }
    public string name { get; set; }
    public string iconFontAlwesome { get; set; }
    public string beforeSlug { get; set; }
}

namespace ArtistiqueCastingAPI.Models;

public class CastingTableModel
{
    public Guid Id { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public List<string> SubCategories { get; set; }
}
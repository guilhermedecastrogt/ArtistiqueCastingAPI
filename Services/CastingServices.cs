namespace ArtistiqueCastingAPI.Services;

public static class CastingServices
{
    public static List<string> ConverteStringSubCategoriesToList(string value)
    {
        string[] subCategoriesArray = value.Split(new string[]{", "}, StringSplitOptions.None);
        return new List<string>(subCategoriesArray);
    }
}
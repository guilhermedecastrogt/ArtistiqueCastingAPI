namespace ArtistiqueCastingAPI;

public class Settings
{
    public static string Secret()
    {
        string? secret = Environment.GetEnvironmentVariable("SecretKeyToWorkWithJWT");
        if (secret == null)
            secret = "61149D5F-6516-4CE1-9DA6-049A42268696";
        else Console.WriteLine("Secret key received from environment variable");
        return secret;
    }
}
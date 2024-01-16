using System.Security.Cryptography;
using System.Text;

namespace SliftioHub.Helpers;

public static class Criptography
{
    public static string HashGenerate(this string value)
    {
        var hash = SHA1.Create();
        var encoding = new ASCIIEncoding();
        var array = encoding.GetBytes(value);
        
        hash.ComputeHash(array);

        var strHexa = new StringBuilder();

        foreach (var item in array)
        {
            strHexa.Append(item.ToString("x2"));
        }

        return strHexa.ToString();
    }
}
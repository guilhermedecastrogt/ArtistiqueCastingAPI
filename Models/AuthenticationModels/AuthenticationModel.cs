using System.ComponentModel.DataAnnotations;
using ArtistiqueCastingAPI.Enums;

namespace ArtistiqueCastingAPI.Models;

public class AuthenticationModel
{
    public AuthenticationModel()
    {
        Id = Guid.NewGuid();
    }
    [Key] public Guid Id { get; set; }
    public string Password { get; set; }
    public AuthenticationRoleEnum Role { get; set; }
}
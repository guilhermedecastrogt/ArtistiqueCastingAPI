using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using ArtistiqueCastingAPI.Services.TokenServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliftioHub.Helpers;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/authentication")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationRepository _authenticationRepository;

    public AuthenticationController()
    {
        _authenticationRepository = new AuthenticationRepository();
    }
    
    [HttpPost]
    [Route("add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] AuthenticationModel model)
    {
        if (ModelState.IsValid)
        {
            model.Password = model.Password.HashGenerate();
            await _authenticationRepository.Add(model);
            return Ok(model);
        }
        return BadRequest(new { message = "Invalid model" });
    }
    
    [HttpPost]
    public async Task<ActionResult<dynamic>> Login([FromBody] AuthenticationModel model)
    {
        try
        {
            AuthenticationModel? authentication = await _authenticationRepository.GetLogin(model);
            if (authentication == null) return NotFound(new{message = "Authentication not found"});
            string token = TokenService.GenerateToken(authentication);
            return new { token = token };
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex});
        }
    }
}
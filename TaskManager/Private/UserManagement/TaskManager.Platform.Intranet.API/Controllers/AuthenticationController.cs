using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace TaskManager.Platform.Intranet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("google")]
    public IActionResult GoogleAuthentication()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback", "Auth", null, Request.Scheme)
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (authenticateResult.Succeeded)
        {
            // Authentication succeeded, redirect authenticated user to desired page
            return Redirect("/dashboard");
        }

        LogAuthenticationResultDetails(authenticateResult);

        return BadRequest("Authentication failed.");
    }

    private void LogAuthenticationResultDetails(AuthenticateResult authenticateResult)
    {
        if (authenticateResult == null)
        {
            _logger.LogError("Authentication result is null.");
            return;
        }

        _logger.LogError("Authentication failed with the following details:");
        _logger.LogError($"Succeeded: {authenticateResult.Succeeded}");
        _logger.LogError($"Failure message: {authenticateResult.Failure?.InnerException}");
        _logger.LogError($"Failure message: {authenticateResult.Failure?.HResult}");
        _logger.LogError($"Failure message: {authenticateResult.Failure?.StackTrace}");


        // Log more properties as needed
    }
}

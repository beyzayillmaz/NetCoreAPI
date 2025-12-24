using Microsoft.AspNetCore.Mvc;
using NetCoreAPI.Models;
using NetCoreAPI.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    [HttpPost("Login")]
    public IActionResult Login(LoginRequest request)
    {
        // 1️⃣ Fake user (şimdilik)
        if (request.Email != "test@test.com" || request.Password != "123456")
            return Unauthorized("Email veya şifre hatalı");

        // 2️⃣ Kullanıcı doğru → token üret
        var token = _tokenService.CreateToken(
            userId: 1,
            email: request.Email,
            role: "Admin"
        );

        // 3️⃣ Token’ı client’a dön (Düzenlenecek)
        return Ok(new
        {
            accessToken = token
        });
    }
}
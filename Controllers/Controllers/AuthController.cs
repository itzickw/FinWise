using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModel;
using UserManager;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager.UserManager _userManager;

    public UsersController(UserManager.UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User userDto)
    {
        var result = await _userManager.RegisterUserAsync(userDto.Email, userDto.Password);

        if (result == "User already exists")
            return Conflict(new { message = result });

        if (result.StartsWith("Registration failed"))
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }


    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser([FromQuery] string email)
    {
        Console.WriteLine($"📨 got mail to delete: '{email}'");

        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("❌ יש לספק כתובת אימייל תקינה.");
        }

        bool success = await _userManager.DeleteUserAsync(email);

        if (success)
        {
            return Ok("✅ המשתמש נמחק בהצלחה.");
        }
        else
        {
            return NotFound("❌ המשתמש לא נמצא או לא ניתן למחוק אותו.");
        }
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return BadRequest("❌ אימייל וסיסמה חדשים הם שדות חובה.");
        }

        bool isUpdated = await _userManager.UpdateUserPasswordAsync(request.Email, request.NewPassword);

        if (!isUpdated)
        {
            return NotFound("❌ לא נמצא המשתמש או שהעדכון נכשל.");
        }

        return Ok("✅ סיסמת המשתמש עודכנה בהצלחה.");
    }
}

public class UpdatePasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}



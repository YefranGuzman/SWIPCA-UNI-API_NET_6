using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SWIPCA_UNI_API.DataAccess;
using System.Text;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        private readonly DA_Usuario _daUsuario;
        private readonly DA_Usuario.JwtService _jwtService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _config;
        public Usuario(DA_Usuario daUsuario, DA_Usuario.JwtService jwtService,UserManager<Usuario> userManager, IConfiguration config)
        {
            _daUsuario = daUsuario;
            _jwtService = jwtService;
            _userManager = userManager;
            _config = config;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var usuario = await _daUsuario.ObtenerPorNick(model.Nick);

            if (usuario == null || !_daUsuario.VerificarContrasena(usuario, model.Contrasena))
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerarToken(usuario);

            return Ok(new { token });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost("Recuperar-password")]
        public async Task<IActionResult> RecuperarPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("No se ha encontrado el usuario vinculado al correo");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var resetUrl = $"{_config["AppUrl"]}/reset-password?email={email}&token={validToken}";

            // La variable resetURL queda pendiente de modificar debido a que no tengo idea de como devolver el url
            // ...

            return Ok("Correo de reinicio de contraseña enviado.");
        }

        [HttpPost("reiniciar-password")]
        public async Task<IActionResult> Reiniciar_Password(string email, string password, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("No se ha encontrado el usuario vinculado al correo");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var validToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, validToken, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Contraseña Reiniciada Exitosamente");
        }

    }

    public class LoginRequest
    {
        public string Nick { get; set; }
        public string Contrasena { get; set; }
    }
}

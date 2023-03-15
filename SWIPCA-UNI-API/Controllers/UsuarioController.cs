using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;
using System.Threading.Tasks;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        private readonly DA_Usuario _daUsuario;
        private readonly DA_Usuario.JwtService _jwtService;

        public Usuario(DA_Usuario daUsuario, DA_Usuario.JwtService jwtService)
        {
            _daUsuario = daUsuario;
            _jwtService = jwtService;
        }

        [HttpPost]
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
    }

    public class LoginRequest
    {
        public string Nick { get; set; }
        public string Contrasena { get; set; }
    }
}

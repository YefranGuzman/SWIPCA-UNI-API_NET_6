using SWIPCA_UNI_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Usuario
    {
        DbCargaAcademicaContext cn = new();

        public async Task<Usuario> ObtenerPorNick(string Nick)
        {
            return await cn.Usuarios.SingleOrDefaultAsync(x => x.Nick == Nick);
        }

        public bool VerificarContrasena(Usuario usuario, string contrasena)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            var resultado = passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasena, contrasena);

            return resultado == PasswordVerificationResult.Success;
        }

        public class JwtService
        {
            private readonly IConfiguration _configuration;

            public JwtService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerarToken(Usuario usuario)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                    new Claim("rol", usuario.TipoRolNavigation.Titulo),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }


    }
}

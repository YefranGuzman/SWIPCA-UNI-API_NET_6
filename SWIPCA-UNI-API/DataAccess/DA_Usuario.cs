using SWIPCA_UNI_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Usuario
    {
        DbCargaAcademicaContext cn = new();
        private static List<string> activeTokens = new List<string>();
        public async Task<int> ObtenerDocente(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Departamentos on a.IdUsuario equals b.Jefe
                                 where a.IdUsuario == idUsuario
                                 select b.Jefe).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<int> ObtenerJefeDepartamento(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Departamentos on a.IdUsuario equals b.Jefe
                                 where a.IdUsuario == idUsuario
                                 select b.Jefe).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<int> ObtenerVicedecano(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Facultads on a.IdUsuario equals b.Vice
                                 where a.IdUsuario == idUsuario
                                 select b.Vice).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<Usuario> ObtenerPorNick(string Nick)
        {
            var usuario = await cn.Usuarios.Include(u => u.TipoRolNavigation)
                                    .FirstOrDefaultAsync(x => x.Nick == Nick);
            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con ese Nick.");
            }
            return usuario;
        }
        public bool VerificarContrasena(Usuario usuario, string contrasena)
        {
            string hashedPassword = HashPassword(contrasena); // Generar el hash de la contraseña ingresada

            return usuario.PasswordHash == hashedPassword; // Comparar el hash almacenado con el hash generado
        }
        private string HashPassword(string contrasena)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", usuario.IdUsuario.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Agregar el token a la lista de activeTokens
                activeTokens.Add(tokenString);

                return tokenString;
            }
        }
        public static void RemoveToken(string token)
        {
            activeTokens.Remove(token);
        }
    }
}

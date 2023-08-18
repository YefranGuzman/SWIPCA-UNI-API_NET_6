using SWIPCA_UNI_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Usuario
    {
        DbCargaAcademicaContext cn = new();
        private static List<string> activeTokens = new List<string>();
        public async Task<Usuario> ObtenerUsuarioDocente(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 where a.IdUsuario == idUsuario && a.IdRol == 1 && a.Estado == 1
                                 select a.PrimerNombre + " " + a.PrimerApellido).FirstOrDefaultAsync();


            return null;
        }
        public async Task<Usuario> ObtenerDocente(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Docentes on a.IdUsuario equals b.IdDocente
                                 where a.IdUsuario == idUsuario
                                 select a).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<Usuario> AgregarUsuario(Usuario usuario)
        {
            usuario.PasswordHash = HashPassword(usuario.Contrasena);
            usuario.UserName = usuario.PrimerNombre + " " + usuario.SegundoNombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido;
            usuario.NormalizedUserName = usuario.UserName;

            await cn.AddAsync(usuario);
            await cn.SaveChangesAsync();
            return usuario;
        }
        public async Task<Usuario> ObtenerJefeDepartamento(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Departamentos on a.IdUsuario equals b.Jefe
                                 where a.IdUsuario == idUsuario
                                 select a).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<Usuario> ObtenerVicedecano(int idUsuario)
        {
            var usuario = await (from a in cn.Usuarios
                                 join b in cn.Facultads on a.IdUsuario equals b.Vice
                                 where a.IdUsuario == idUsuario
                                 select a).FirstOrDefaultAsync();

            return usuario;
        }
        public async Task<Usuario> ObtenerPorUserName(string UserName)
        {
            var usuario = await cn.Usuarios.FirstOrDefaultAsync(x => x.UserName == UserName);

            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con ese UserName.");
            }
            return usuario;
        }
        public bool VerificarContrasena(Usuario usuario, string contrasena)
        {
            string hashedPassword = HashPassword(contrasena);

            return usuario.PasswordHash == hashedPassword;
        }
        private static string HashPassword(string contrasena)
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

        public class UsuarioI_DTO
        {
            [JsonProperty("nombrecompleto")]
            public string? nombrecompleto { get; set; }
            [JsonProperty("puesto")]
            public string? puesto { get; set; }
            [JsonProperty("cargo")]
            public string? cargo { get; set; } 
        }
    }
}

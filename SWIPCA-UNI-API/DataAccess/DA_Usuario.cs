using SWIPCA_UNI_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Usuario
    {
        DbCargaAcademicaContext cn = new();
        private static List<string> activeTokens = new List<string>();
        public async Task<UsuarioI_DTO> ObtenerInformacionUsuario(int idUsuario)
        {
            var rolUsuario = await (from a in cn.Usuarios
                                 where a.IdUsuario == idUsuario
                                 select a.TipoRol).FirstOrDefaultAsync();

            if (rolUsuario == 2)
            {
                var informacionUsuario = await (from a in cn.Usuarios
                                                join b in cn.Docentes
                                                on a.IdUsuario equals b.IdUsuario
                                                join c in cn.Departamentos
                                                on b.IdDepartamento equals c.IdDepartamento
                                                where a.IdUsuario == idUsuario
                                                select new UsuarioI_DTO
                                                {
                                                    nombrecompleto = a.UserName,
                                                    puesto = "Docente",
                                                    cargo = c.Nombre
                                                }).FirstAsync();
                return informacionUsuario;

            } if (rolUsuario == 3)
            {
                var informacionUsuario = await (from a in cn.Usuarios
                                                join b in cn.Departamentos
                                                on a.IdUsuario equals b.Jefe
                                                where a.IdUsuario == idUsuario
                                                select new UsuarioI_DTO
                                                {
                                                    nombrecompleto = a.UserName,
                                                    puesto = "Jefe Departamento",
                                                    cargo = b.Nombre
                                                }).FirstAsync();
                return informacionUsuario;
            } if(rolUsuario == 4)
            {
                var informacionUsuario = await (from a in cn.Usuarios
                                                join b in cn.Facultads
                                                on a.IdUsuario equals b.Vice
                                                where a.IdUsuario == idUsuario
                                                select new UsuarioI_DTO
                                                {
                                                    nombrecompleto = a.UserName,
                                                    puesto = "Vicedecando",
                                                    cargo = b.Nombre
                                                }).FirstAsync();
                return informacionUsuario;
            } else if(rolUsuario==5)
            {
                var informacionUsuario = await (from a in cn.Usuarios
                                                where a.IdUsuario == idUsuario
                                                select new UsuarioI_DTO
                                                {
                                                    nombrecompleto = a.UserName,
                                                    puesto = "Docente",
                                                    cargo = "Administrador"
                                                }).FirstAsync();
                return informacionUsuario;
            }

            throw new InvalidOperationException(message: "No hay usuario valido");
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
        public async Task<Usuario> ObtenerPorNick(string Nick)
        {
            var usuario = await cn.Usuarios.Include(u => u.TipoRol)
                                    .FirstOrDefaultAsync(x => x.Nick == Nick);
            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con ese Nick.");
            }
            return usuario;
        }
        public bool VerificarContrasena(Usuario usuario, string contrasena)
        {
            string hashedPassword = HashPassword(contrasena);

            return usuario.PasswordHash == hashedPassword;
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

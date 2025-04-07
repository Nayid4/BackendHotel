using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dominio.ObjetosDeValor;
using Dominio.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacion.Utilidades
{
    public class GestionToken : IGestionToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GestionToken(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string EncryptSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el Hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string GenerateJWT(Usuario usuario, int opcion)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentias = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            // Crear la informacion del usuario para token
            var UserClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.Valor.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre + ' ' + usuario.Apellido),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim(ClaimTypes.GivenName, usuario.NombreDeUsuario),
            };

            // Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                _configuration["Jwt:issuer"],
                _configuration["Jwt:Audience"],
                claims: UserClaims,
                expires: opcion switch
                {
                    1 => DateTime.Now.AddMinutes(60),
                    2 => DateTime.Now.AddMinutes(80),
                    _ => throw new ArgumentOutOfRangeException(nameof(opcion), "Opción no válida.")
                },
                signingCredentials: credentias
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        public DatosUsuarioDTO? DecodeJWT()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return null;
            }

            var userClaims = identity.Claims;

            var idClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var nombreClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
            var roleClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;
            var nombreDeUsuarioClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.GivenName)?.Value;

            if (
                Guid.TryParse(idClaim, out var id) &&
                !string.IsNullOrEmpty(nombreClaim) &&
                !string.IsNullOrEmpty(roleClaim) &&
                !string.IsNullOrEmpty(nombreDeUsuarioClaim)
            )
            {
                return new DatosUsuarioDTO(
                    id,
                    nombreClaim,
                    roleClaim,
                    nombreDeUsuarioClaim
                );
            }

            return null;
        }
    }
}

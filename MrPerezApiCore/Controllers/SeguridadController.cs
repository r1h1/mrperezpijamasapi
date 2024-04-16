using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrPerezApiCore.Data;
using MrPerezApiCore.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace MrPerezApiCore.Controllers
{
    [EnableCors("NuevaPolitica")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly AutenticacionData _autenticacionData;

        public SeguridadController(IConfiguration config, AutenticacionData AutenticacionData)
        {
            _secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _autenticacionData = AutenticacionData;
        }

        //POST METHOD AUTHENTICATED
        [HttpPost]
        [Route("/Seguridad/Validar")]
        public async Task<IActionResult> Comparar([FromBody] Seguridad request)
        {
            if (!string.IsNullOrEmpty(request.Usuario) && !string.IsNullOrEmpty(request.Clave))
            {
                IActionResult response = await ValidarUsuario(request.Usuario, request.Clave);

                if (response is ObjectResult objectResult && objectResult.Value is Autenticacion objeto)
                {
                    byte[] bytesClave = Encoding.UTF8.GetBytes(request.Clave);
                    byte[] hashClave;

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        hashClave = sha256.ComputeHash(bytesClave);
                    }

                    string claveEncriptada = Convert.ToBase64String(hashClave);

                    if (string.Equals(objeto.Usuario, request.Usuario, StringComparison.Ordinal) &&
                        string.Equals(objeto.Clave, claveEncriptada, StringComparison.Ordinal))
                    {
                        var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                        var claims = new ClaimsIdentity();
                        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Usuario));

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = claims,
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                        string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                        //EXISTE EL USUARIO
                        return StatusCode(StatusCodes.Status201Created, new { 
                            code = 201, 
                            message = "Created", 
                            objeto.AutenticacionId,
                            objeto.Usuario,
                            objeto.Clave,
                            objeto.Estado,
                            token = tokenCreado 
                        });
                    }
                    else
                    {
                        //NO EXISTE EL USUARIO
                        return StatusCode(StatusCodes.Status400BadRequest, new { code = 400, message = "BadRequest", token = "" });
                    }
                }
                else
                {
                    //NO EXISTE RETORNO DEL OBJETO DE VALIDAR USUARIO
                    return StatusCode(StatusCodes.Status204NoContent, new { code = 204, message = "NoContent", token = "NoContent" });
                }
            }
            else
            {
                //NO TIENE DATOS PORQUE NO ESTÁ LLENO
                return StatusCode(StatusCodes.Status204NoContent, new { code = 204, message = "NoContent", token = "NoContent" });
            }
        }

        //GET WITH USUARIO AND CLAVE METHOD
        [HttpGet("{usuario}/{clave}")]
        public async Task<IActionResult> ValidarUsuario(string usuario, string clave)
        {
            Autenticacion objeto = await _autenticacionData.ValidarUsuario(usuario, clave);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }
    }
}

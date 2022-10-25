using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reportes.DAO;
using Reportes.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiReportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioDAO _usuarioDao;
        private readonly IMapper _mapper;

        public TokenController(IConfiguration configuration, IUsuarioDAO usuarioDao, IMapper mapper)
        {
            _configuration = configuration;
            _usuarioDao = usuarioDao;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDTO usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Usuario) && !string.IsNullOrEmpty(usuario.Contrasenia))
            {
                var usuarioLogueado = await _usuarioDao.ObtieneUsuarioPorCredenciales(usuario);

                if (!string.IsNullOrEmpty(usuarioLogueado.Nombre))
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", usuarioLogueado.Login.ToString()),
                    new Claim("DisplayName", usuarioLogueado.Nombre.ToString()),
                    new Claim("UserName", usuarioLogueado.Login.ToString()),
                    new Claim("Email", usuarioLogueado.Email.ToString())
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                        );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
    }
}

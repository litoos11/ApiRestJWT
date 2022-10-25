using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reportes.DAO;
using Reportes.Entity;

namespace ApiReportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioDAO _usuarioDao;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioDAO usuarioDAO, IMapper mapper)
        {
            _usuarioDao = usuarioDAO;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            List<Usuario> usuarios = await _usuarioDao.ObtieneTotos();
            return _mapper.Map<List<Usuario>, List<UsuarioDTO>>(usuarios);
        }
    }
}

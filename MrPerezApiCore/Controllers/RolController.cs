using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrPerezApiCore.Data;
using MrPerezApiCore.Models;

namespace MrPerezApiCore.Controllers
{
    [EnableCors("NuevaPolitica")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly RolData _rolData;

        public RolController(RolData RolData)
        {
            _rolData = RolData;
        }

        //GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Rol> Lista = await _rolData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Rol objeto = await _rolData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Rol objeto)
        {
            bool respuesta = await _rolData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Rol objeto)
        {
            bool respuesta = await _rolData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

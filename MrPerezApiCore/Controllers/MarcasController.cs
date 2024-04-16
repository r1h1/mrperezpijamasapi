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
    public class MarcasController : ControllerBase
    {
        private readonly MarcasData _marcasData;

        public MarcasController(MarcasData RolData)
        {
            _marcasData = RolData;
        }

        //GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Marcas> Lista = await _marcasData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Marcas objeto = await _marcasData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Marcas objeto)
        {
            bool respuesta = await _marcasData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Marcas objeto)
        {
            bool respuesta = await _marcasData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

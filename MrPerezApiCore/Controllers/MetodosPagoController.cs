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
    public class MetodosPagoController : ControllerBase
    {
        private readonly MetodosPagoData _metodosPagoData;

        public MetodosPagoController(MetodosPagoData MetodosPagoData)
        {
            _metodosPagoData = MetodosPagoData;
        }

        //GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<MetodosPago> Lista = await _metodosPagoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            MetodosPago objeto = await _metodosPagoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MetodosPago objeto)
        {
            bool respuesta = await _metodosPagoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] MetodosPago objeto)
        {
            bool respuesta = await _metodosPagoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

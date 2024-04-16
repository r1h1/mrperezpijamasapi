using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrPerezApiCore.Data;
using MrPerezApiCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrPerezApiCore.Controllers
{
    [EnableCors("NuevaPolitica")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenesData _ordenesData;

        public OrdenController(OrdenesData OrdenesData)
        {
            _ordenesData = OrdenesData;
        }

        // GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Ordenes> lista = await _ordenesData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Ordenes objeto = await _ordenesData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        // POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Ordenes objeto)
        {
            bool respuesta = await _ordenesData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        // PUT METHOD
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Ordenes objeto)
        {
            bool respuesta = await _ordenesData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
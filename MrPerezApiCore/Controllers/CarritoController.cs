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
    public class CarritoController : ControllerBase
    {
        private readonly CarritoData _carritoData;

        public CarritoController(CarritoData carritoData)
        {
            _carritoData = carritoData;
        }

        // GET: api/Carrito
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Carrito> lista = await _carritoData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Carrito/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Carrito objeto = await _carritoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        // POST: api/Carrito
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Carrito objeto)
        {
            bool respuesta = await _carritoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        // PUT: api/Carrito
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Carrito objeto)
        {
            bool respuesta = await _carritoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
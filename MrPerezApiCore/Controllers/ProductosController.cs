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
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosData _productosData;

        public ProductosController(ProductosData ProductosData)
        {
            _productosData = ProductosData;
        }

        //GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ProductosSelect> listaProductos = await _productosData.Lista();
            int cantidadProductos = listaProductos.Count;
            var respuesta = new { cantidadTotalProductos = cantidadProductos, productosData = listaProductos };
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            ProductosSelect objeto = await _productosData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear([FromBody] Productos objeto)
        {
            bool respuesta = await _productosData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Productos objeto)
        {
            bool respuesta = await _productosData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

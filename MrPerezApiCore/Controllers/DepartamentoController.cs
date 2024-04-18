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
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoData _departamentoData;

        public DepartamentoController(DepartamentoData departamentoData)
        {
            _departamentoData = departamentoData;
        }

        //GET METHOD
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Lista()
        {
            List<Departamento> Lista = await _departamentoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Obtener(int id)
        {
            Departamento objeto = await _departamentoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear([FromBody] Departamento objeto)
        {
            bool respuesta = await _departamentoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Departamento objeto)
        {
            bool respuesta = await _departamentoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

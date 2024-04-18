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
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaData _empresaData;

        public EmpresaController(EmpresaData empresaData)
        {
            _empresaData = empresaData;
        }

        //GET METHOD
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Lista()
        {
            List<Empresa> Lista = await _empresaData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Obtener(int id)
        {
            Empresa objeto = await _empresaData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        //POST METHOD
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear([FromBody] Empresa objeto)
        {
            bool respuesta = await _empresaData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Empresa objeto)
        {
            bool respuesta = await _empresaData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

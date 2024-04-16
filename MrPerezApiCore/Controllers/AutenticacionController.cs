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
    public class AutenticacionController : ControllerBase
    {
        private readonly AutenticacionData _autenticacionData;

        public AutenticacionController(AutenticacionData AutenticacionData)
        {
            _autenticacionData = AutenticacionData;
        }

        //GET METHOD
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Autenticacion> Lista = await _autenticacionData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Autenticacion objeto = await _autenticacionData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }        

        //POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Autenticacion objeto)
        {
            bool respuesta = await _autenticacionData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        //PUT METHOD
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Autenticacion objeto)
        {
            bool respuesta = await _autenticacionData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

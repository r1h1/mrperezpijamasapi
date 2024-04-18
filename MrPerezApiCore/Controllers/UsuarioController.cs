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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioData _usuarioData;

        public UsuarioController(UsuarioData UsuarioData)
        {
            _usuarioData = UsuarioData;
        }

        //GET METHOD
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Lista()
        {
            List<Usuario> Lista = await _usuarioData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        //GET WITH ID METHOD
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Obtener(int id)
        {
            Usuario objeto = await _usuarioData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        // POST METHOD
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Usuario objeto)
        {
            var resultadoInsercion = await _usuarioData.Crear(objeto);
            bool insercionExitosa = resultadoInsercion.Item1;
            int ultimoIdInsertado = resultadoInsercion.Item2;

            return StatusCode(StatusCodes.Status200OK, new { insertedId = ultimoIdInsertado, isSuccess = insercionExitosa });
        }


        //PUT METHOD
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Editar([FromBody] Usuario objeto)
        {
            bool respuesta = await _usuarioData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

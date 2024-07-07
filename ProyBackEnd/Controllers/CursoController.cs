using Microsoft.AspNetCore.Mvc;
using ProyBackEnd.Models;

namespace ProyBackEnd.Controllers
{
    [ApiController]
    [Route("Curso")]
    public class CursoController : ControllerBase
    {
        [Route("ListarCurso")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarCurso()
        {
            try
            {
                var lista = await new CursoDto().ListarCurso();
                return Ok(lista);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

using Capa_Entidad;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using ProyBackEnd.Models;

namespace ProyBackEnd.Controllers
{
    [ApiController]
    [Route("Alumno")]
    public class AlumnoController : ControllerBase
    {
        [Route("ListarAlumno")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ListarAlumno(int orden, int idalumno)
        {
            try
            {
                var Lista = await new AlumnoDto().ListarAlumno(orden, idalumno);
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ListarAlumno_XID")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ListarAlumno_XID(int orden, int idalumno)
        {
            try
            {
                var Lista = await new AlumnoDto().ListarAlumno_XID(orden, idalumno);
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("RegistrarAlumno")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarAlumno([FromBody] AlumnoEnt objalumno)
        {
            try
            {
                ResultadoTrasationE respuesta = new ResultadoTrasationE();
                if (objalumno.IdAlumno == 0)
                {
                    respuesta = await new AlumnoDto().RegistrarAlumno(objalumno);

                }
                else
                {
                    respuesta = await new AlumnoDto().ActualizarAlumno(objalumno);
                }

                return Ok(objalumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

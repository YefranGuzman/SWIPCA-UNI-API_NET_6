using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;


namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadesController : ControllerBase
    {
        private readonly DA_Disponibilidad disponibilidad;
        private readonly DA_Usuario Usuarios;

        public DisponibilidadesController(DA_Disponibilidad daDisponibilidad, DA_Usuario dA_Usuario)
        {
            disponibilidad = daDisponibilidad;
            Usuarios = dA_Usuario?? new DA_Usuario();
        }
        

        [HttpPut("Disponibilidad/PutGuardarDisponibilidadDocente")]
        public async Task<IActionResult> PutGuardarDisponibilidadDocente(int idDocente, string observacion, int periodo, string evidencia, int estado, int tipoJustificacion)
        {
            var result = await disponibilidad.GuardarDisponibilidadDocente(idDocente, observacion, periodo, evidencia, estado, tipoJustificacion);

            if (result == "El Id del docente no puede venir vacio")
            {
                return BadRequest(result);
            }
            else if (result == "Disponibilidad guardada exitosamente")
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar la disponibilidad");
            }
        }

        [HttpGet("GetObtenerDisponibilidadesPorEstado")]
        public async Task<ActionResult<List<Disponibilidad>>> GetObtenerDisponibilidadesPorEstado(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return BadRequest("El id del docente no es válido.");
            }

            var result = await disponibilidad.ObtenerDisponibilidadesPorEstado(idUsuario);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para el docente especificado.");
            }

            return Ok(result);
        }

        [HttpGet("departamento/{idDepartamento}")]
        public async Task<ActionResult<List<Disponibilidad>>> GetObtenerDisponibilidadesTodosDocentesPorDepartamento(int idDepartamento)
        {
            if (idDepartamento <= 0)
            {
                return BadRequest("El id del departamento no es válido.");
            }

            var result = await disponibilidad.ObtenerDisponibilidadesTodosDocentesPorDepartamento(idDepartamento);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para los docentes del departamento especificado.");
            }
            return Ok(result);
        }
        [HttpGet("departamento")]
        public async Task<ActionResult<List<DA_Disponibilidad.SolicitudDisponibilidadDTO>>> AprobarDisponibilidadesPorDepartamento(int IdUsuario) 
        {
            var Result = await disponibilidad.AprobarDisponibilidades(IdUsuario);

            if (Result == null || !Result.Any())
            {
                return NotFound("No se encuentra ninguna solicitud actualmente");
            }
            return Ok(Result);
        }
        [HttpPost]
        public async Task<bool> CambiarEstadoSolicitudDisponibilidad(int idSolicitud, int nuevoEstado)
        {
            await disponibilidad.ActualizarEstadoSolicitudDisponibilidad(idSolicitud, nuevoEstado);

            return true;
        }

    }
}

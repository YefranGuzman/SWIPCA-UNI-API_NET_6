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

        [HttpPost("PostGuardarDisponibilidadDocente")]
        public async Task<IActionResult> GetGuardarDisponibilidadDocente(int IdUsuario, string observacion, int periodo, string evidencia, int estado, int tipoJustificacion)
        {
            var result = await disponibilidad.GuardarDisponibilidadDocente(IdUsuario, observacion, periodo, evidencia, estado, tipoJustificacion);

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

        [HttpGet("GetObtenerDisponibilidades")]
        public async Task<ActionResult<List<Disponibilidad>>> GetObtenerDisponibilidades(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return BadRequest("El id del docente no es válido.");
            }

            var result = await disponibilidad.ObtenerDisponibilidades(idUsuario);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para el docente especificado.");
            }

            return Ok(result);
        }

        [HttpPut("PutAprobarDisponibilidad")]
        public async Task<ActionResult<List<DA_Disponibilidad.SolicitudDisponibilidadDTO>>> AprobarDisponibilidadesPorDepartamento(int IdUsuario, int IdSolicitud) 
        {
            int NuevoEstado = 2;

            var Result = await disponibilidad.ActualizarEstadoSolicitudDisponibilidad(IdUsuario, IdSolicitud ,NuevoEstado);

            if (Result == null)
            {
                return NotFound("No se encuentra ninguna solicitud actualmente");
            }
            return Ok(Result);
        }
        [HttpPost("PostActualizarSolicitud")]
        public async Task<ActionResult<string>> CambiarEstadoSolicitudDisponibilidad(int idUsuario, int idSolicitud, int nuevoEstado)
        {
            if (nuevoEstado == 1 || nuevoEstado == 3)
            {
                await disponibilidad.ActualizarEstadoSolicitudDisponibilidad(idUsuario, idSolicitud, nuevoEstado);

                if (nuevoEstado == 1)
                {
                    return Ok("La petición se encuentra en proceso."); // 200 OK - Peticion en proceso
                }
                else
                {
                    return StatusCode(210, "La petición ha sido rechazada."); // 210 - Código de estado personalizado para rechazo
                }
            }
            else if (nuevoEstado == 2)
            {
                await disponibilidad.ActualizarEstadoSolicitudDisponibilidad(idUsuario, idSolicitud, nuevoEstado);
                return Ok("La Peticion ha sido aprobada"); // 200 No Content - Aprobada
            }

            return BadRequest("Operacion inválido");
        }
    }
}

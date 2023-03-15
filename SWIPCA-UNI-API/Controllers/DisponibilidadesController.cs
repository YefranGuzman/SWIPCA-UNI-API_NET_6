using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    public class DisponibilidadesController : Controller
    {
        [HttpPut("{IdDocente}{Observacion}{Periodo}{Evidencia}{Estado}{TipoJustificacion" +
            "}")]
        public async Task<IActionResult> PUTGuardarDisponibilidadDocente(int IdDocente, string Observacion, int Periodo, string Evidencia, int Estado, int TipoJustificacion)
        {
            var DA_GuardarDisponibilidadDocente = new DA_Disponibilidad();
            var result = await DA_GuardarDisponibilidadDocente.GuardarDisponibilidadDocente(IdDocente, Observacion, Periodo, Evidencia, Estado, TipoJustificacion);

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
        [HttpGet("{idDocente}")]
        public async Task<ActionResult<List<Disponibilidad>>> GETObtenerDisponibilidadesPorEstado(int idDocente)
        {
            if (idDocente <= 0)
            {
                return BadRequest("El id del docente no es válido.");
            }

            var ObtenerDisponibilidadesPorEstado = new DA_Disponibilidad();

            var result = await ObtenerDisponibilidadesPorEstado.ObtenerDisponibilidadesPorEstado(idDocente);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para el docente especificado.");
            }

            return Ok(result);
        }

        [HttpGet("{idDocente}")]
        public async Task<ActionResult<List<Disponibilidad>>> ObtenerDisponibilidadesTodosDocentesPorDepartamento(int idDocente)
        {
            if (idDocente <= 0)
            {
                return BadRequest("El id del docente no es válido.");
            }

            var ObtenerDisponibilidadesTodosDocentesPorDepartamento = new DA_Disponibilidad();

            var result = await ObtenerDisponibilidadesTodosDocentesPorDepartamento.ObtenerDisponibilidadesTodosDocentesPorDepartamento(idDocente);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para los docente.");
            }

            return Ok(result);

            return result;
        }
    }
}

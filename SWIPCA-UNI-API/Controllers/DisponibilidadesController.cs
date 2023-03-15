using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadesController : ControllerBase
    {
        private readonly DA_Disponibilidad _daDisponibilidad;

        public DisponibilidadesController(DA_Disponibilidad daDisponibilidad)
        {
            _daDisponibilidad = daDisponibilidad;
        }

        [HttpPut("{idDocente}/{observacion}/{periodo}/{evidencia}/{estado}/{tipoJustificacion}")]
        public async Task<IActionResult> PutGuardarDisponibilidadDocente(int idDocente, string observacion, int periodo, string evidencia, int estado, int tipoJustificacion)
        {
            var result = await _daDisponibilidad.GuardarDisponibilidadDocente(idDocente, observacion, periodo, evidencia, estado, tipoJustificacion);

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

        [HttpGet("docente/{idDocente}")]
        public async Task<ActionResult<List<Disponibilidad>>> GetObtenerDisponibilidadesPorEstado(int idDocente)
        {
            if (idDocente <= 0)
            {
                return BadRequest("El id del docente no es válido.");
            }

            var result = await _daDisponibilidad.ObtenerDisponibilidadesPorEstado(idDocente);

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

            var result = await _daDisponibilidad.ObtenerDisponibilidadesTodosDocentesPorDepartamento(idDepartamento);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron disponibilidades para los docentes del departamento especificado.");
            }

            return Ok(result);
        }
    }
}

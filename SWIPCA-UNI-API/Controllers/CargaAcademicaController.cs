using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.DataAccess;
using static SWIPCA_UNI_API.DataAccess.DA_CargaAcademica;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/CargaAcademica")]
    [ApiController]
    public class CargaAcademicaController : Controller
    {
        private readonly DA_CargaAcademica DA_CargaAcademica;

        public CargaAcademicaController(DA_CargaAcademica daCargaAcademica)
        {
            DA_CargaAcademica = daCargaAcademica;
        }
        [HttpPost]
        public async Task<IActionResult> AgregarCargaAcademica([FromBody] CargaAcademica cargaAcademica)
        {
            try
            {
                await DA_CargaAcademica.AgregarCargaAcademica(cargaAcademica);
                return Ok("La carga academica se ha cargado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CambiarEstadoCargaAcademicaAprobada(int idCargaAcademica)
        {
            try
            {
                await DA_CargaAcademica.CambiarEstadoCargaAcademicaAprobada(idCargaAcademica);
                return Ok("La carga académica ha sido aprobada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CambiarEstadoCargaAcademicaDenegada(int idCargaAcademica)
        {
            try
            {
                await DA_CargaAcademica.CambiarEstadoCargaAcademicaDenegada(idCargaAcademica);
                return Ok("La carga académica ha sido aprobada exitosamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }
        [HttpGet("{idDocente}")]
        public async Task<ActionResult<List<CargaAcademicaDTO>>> ObtenerCargaAcademicaDocente(int idDocente)
        {
            try
            {
                var cargaAcademica = await DA_CargaAcademica.ObtenerCargaAcademicaDocente(idDocente);

                if (cargaAcademica == null || cargaAcademica.Count == 0)
                {
                    return NotFound("Cargas no encontradadas");
                }

                return Ok(cargaAcademica);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // log the exception
                return StatusCode(500);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;
using static SWIPCA_UNI_API.DataAccess.DA_Docentes;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly DA_Docentes _Docentes;

        public DocentesController(DA_Usuario _Usuario, DA_Docentes _Docente)
        {
            _Docentes = _Docente;
        }
        
        [HttpGet("ObtenerDocente")]
        public async Task<ActionResult<List<string>>> ObtenerDocentes(int idDepartamento, int idFacultad)
        {
            var L_GOD = await _Docentes.ObtenerDocentes(idDepartamento, idFacultad);

            return L_GOD;
        }

        [HttpGet("/disponibilidad_docente")]
        public async Task<ActionResult<List<DisponibilidadDTO>>> ObtenerDisponibilidadDocente(int idUsuario)
        {
            var L_GODD = await _Docentes.ObtenerDisponibilidadDocente(idUsuario);

            return L_GODD;
        }

        [HttpGet("/agenda_docente")]
        public async Task<ActionResult<List<Disponibilidad2DTO>>> ObtenerAgendaDocente(int idUsuario)
        {
            var L_GTAD = await _Docentes.ObtenerAgendaDocente(idUsuario);

            return L_GTAD;
        }
    }
}
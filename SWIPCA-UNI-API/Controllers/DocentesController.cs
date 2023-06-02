using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        [HttpGet("{idDepartamento}/{idFacultad}")]
        public async Task<ActionResult<List<string>>> ObtenerDocentes(int idDepartamento, int idFacultad)
        {
            var GOD = new DA_Docentes();
            var L_GOD = await GOD.ObtenerDocentes(idDepartamento, idFacultad);

            return L_GOD;
        }

        [HttpGet("{idDocente}/disponibilidad")]
        public async Task<ActionResult<List<string>>> ObtenerDisponibilidadDocente(int idUsuario)
        {
            var GODD = new DA_Docentes();
            var L_GODD = await GODD.ObtenerDisponibilidadDocente(idUsuario);

            return L_GODD;
        }

        [HttpGet("{idDocente}/carga-laboral")]

        [HttpGet("{idDocente}/agenda")]
        public async Task<ActionResult<List<string>>> ObtenerAgendaDocente(int idUsuario)
        {
            var GTAD = new DA_Docentes();
            var L_GTAD = await GTAD.ObtenerAgendaDocente(idUsuario);

            return L_GTAD;
        }
    }
}
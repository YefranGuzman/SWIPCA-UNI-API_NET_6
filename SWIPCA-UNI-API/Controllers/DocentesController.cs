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
        public async Task<ActionResult<List<string>>> ObtenerDisponibilidadDocente(int idDocente)
        {
            var GODD = new DA_Docentes();
            var L_GODD = await GODD.ObtenerDisponibilidadDocente(idDocente);

            return L_GODD;
        }

        [HttpGet("{idDocente}/carga-laboral")]
        public async Task<ActionResult<List<string>>> ObtenerCargaDocentesLaboral(int idDocente)
        {
            var GOCD = new DA_Docentes();
            var L_GOCD = await GOCD.ObtenerCargaDocentesLaboral(idDocente);

            return L_GOCD;
        }

        [HttpGet("{idDocente}/agenda")]
        public async Task<ActionResult<List<string>>> ObtenerAgendaDocente(int idDocente)
        {
            var GTAD = new DA_Docentes();
            var L_GTAD = await GTAD.ObtenerAgendaDocente(idDocente);

            return L_GTAD;
        }
    }
}

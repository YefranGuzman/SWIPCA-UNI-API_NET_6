using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    [ApiController]
    [Route("api/Docente")]
    public class DocentesController : Controller
    {
        [HttpGet]
        public async Task<List<string>> GetObtenerDocentes(int idDepartamento, int IdFacultad)
        {
            var GOD = new DA_Docentes();
            var LGOD = await GOD.ObtenerDocentes(idDepartamento,IdFacultad);

            return LGOD;
        }
        [HttpGet]
        public async Task<List<string>> GetObtenerDisponibilidadDocente(int idDocente)
        {
            var GODD = new DA_Docentes();
            var LGODD = await GODD.ObtenerDisponibilidadDocente(idDocente);

            return LGODD;
        }
        [HttpGet]
        public async Task<List<string>> GetObtenerCargaDocentesLaboral (int idDocente)
        {
            var GOCD = new DA_Docentes();
            var LGOCD = await GOCD.ObtenerCargaDocentesLaboral(idDocente);

            return LGOCD;
        }
        public async Task<List<string>> GetObtenerAgendaDocente (int idDocente)
        {
            var GTAD = new DA_Docentes();
            var LGTAD = await GTAD.ObtenerAgendaDocente(idDocente);

            return LGTAD;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : Controller
    {
        [HttpGet("{idDepartamento}/{idFacultad}")]
        public async Task<List<string>> GetObtenerDocentes(int idDepartamento, int IdFacultad)
        {
            var GOD = new DA_Docentes();
            var L_GOD = await GOD.ObtenerDocentes(idDepartamento,IdFacultad);

            return L_GOD;
        }
        [HttpGet("{idDepartamento}")]
        public async Task<List<string>> GetObtenerDisponibilidadDocente(int idDocente)
        {
            var GODD = new DA_Docentes();
            var L_GODD = await GODD.ObtenerDisponibilidadDocente(idDocente);

            return L_GODD;
        }
        [HttpGet("{idDepartamento}")]
        public async Task<List<string>> GetObtenerCargaDocentesLaboral (int idDocente)
        {
            var GOCD = new DA_Docentes();
            var L_GOCD = await GOCD.ObtenerCargaDocentesLaboral(idDocente);

            return L_GOCD;
        }
        [HttpGet("{idDepartamento}")]
        public async Task<List<string>> GetObtenerAgendaDocente (int idDocente)
        {
            var GTAD = new DA_Docentes();
            var L_GTAD = await GTAD.ObtenerAgendaDocente(idDocente);

            return L_GTAD;
        }
    }
}

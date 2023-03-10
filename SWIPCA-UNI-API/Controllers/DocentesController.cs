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
            var ObtenerDocentes = new DA_Docentes();
            var ListaDocentes = await ObtenerDocentes.ObtenerDocentes(idDepartamento,IdFacultad);

            return ListaDocentes;
        }
        [HttpGet]
        public async Task<List<string>> GetObtenerDisponibilidadDocente(int idDocente)
        {
            var ObtenerDisponibilidadDocente = new DA_Docentes();
            var ListaObtenerDisponibilidadDocente = await ObtenerDisponibilidadDocente.ObtenerDisponibilidadDocente(idDocente);

            return ListaObtenerDisponibilidadDocente;
        }
        [HttpGet]
        public async Task<List<string>> GetObtenerCargaDocentesLaboral (int idDocente)
        {
            var ObtenerCargaDocentes = new DA_Docentes();
            var ListaObtenerCargaDocentes = await ObtenerCargaDocentes.ObtenerCargaDocentes(idDocente);

            return ListaObtenerCargaDocentes;
        }
    }
}

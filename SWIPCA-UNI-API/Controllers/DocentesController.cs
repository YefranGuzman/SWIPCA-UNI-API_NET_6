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
        private readonly DA_Usuario _Usuario;
        private readonly DA_Docentes _Docentes;
        
        [HttpGet("{idDepartamento}/{idFacultad}")]
        public async Task<ActionResult<List<string>>> ObtenerDocentes(int idDepartamento, int idFacultad)
        {
            var L_GOD = await _Docentes.ObtenerDocentes(idDepartamento, idFacultad);

            return L_GOD;
        }

        [HttpGet("{idDocente}/disponibilidad")]
        public async Task<ActionResult<List<string>>> ObtenerDisponibilidadDocente(int idUsuario)
        {
            var usuario = await _Usuario.ObtenerDocente(idUsuario);
            var L_GODD = await _Docentes.ObtenerDisponibilidadDocente(usuario);

            return L_GODD;
        }

        [HttpGet("{idDocente}/carga-laboral")]

        [HttpGet("{idDocente}/agenda")]
        public async Task<ActionResult<List<string>>> ObtenerAgendaDocente(int idUsuario)
        {
            var usuario = await _Usuario.ObtenerDocente(idUsuario);
            var L_GTAD = await _Docentes.ObtenerAgendaDocente(usuario);

            return L_GTAD;
        }
    }
}
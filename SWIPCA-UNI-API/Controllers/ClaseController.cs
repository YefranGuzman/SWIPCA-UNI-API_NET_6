using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;
using static SWIPCA_UNI_API.DataAccess.DA_Clase;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseController : Controller
    {
        private readonly DA_Clase DA_Clase;
        private readonly DA_Asignatura DA_Asignatura;


        public ClaseController(DA_Clase _clase, DA_Asignatura _asignatura)
        {
            DA_Clase = _clase;
            DA_Asignatura = _asignatura;
        }
        [HttpGet("listar")]
        public async Task<ActionResult<List<Clase>>> GetListarClases()
        {
            var ListaClase = await DA_Clase.ListarClases();

            return (ListaClase);
        }
        [HttpGet("agenda")]
        public async Task<List<AgendaDTO>> GetAgendaDTOs(int idUsuario)
        {
            var ListarAgendaDTO = await DA_Clase.ObtenerAgenda(idUsuario);

            return (ListarAgendaDTO);
        }
    }
}

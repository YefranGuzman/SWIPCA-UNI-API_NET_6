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
        [HttpGet("listar")]
        public async Task<ActionResult<List<Clase>>> Get()
        {
            var clase = new DA_Clase();
            var ListaClase = await clase.ListarClases();

            return (ListaClase);
        }
        [HttpGet("agenda")]
        public async Task<List<AgendaDTO>> GetAgendaDTOs(int idUsuario)
        {
            var agenda = new DA_Clase();
            var ListarAgendaDTO = await agenda.ObtenerAgenda(idUsuario);

            return (ListarAgendaDTO);
        }
    }
}

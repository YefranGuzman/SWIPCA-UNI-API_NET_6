using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.DataAccess;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulaController : Controller
    {
        [HttpGet]
        public async Task<List<Aula>> GetListaAula()
        {
            var list = new DA_Aula();
            var Asignaturas = await list.ListarAula();

            return Asignaturas;
        }
        [HttpGet("{IdDocente}")]
        public async Task<List<Aula>> GetListarAulaPorFacultad(int IdDocente)
        {
            var list = new DA_Aula();
            var Asignatura = await list.ListarAulaPorFacultad(IdDocente);
            return Asignatura;
        }

    }
}

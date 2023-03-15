using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    public class GrupoController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<Grupo>>> GetObtenerGrupo()
        {
            var DA_Grupos = new DA_Grupo();
            var result = DA_Grupos.ObtenerGrupo();

            return Ok(result);
        }
    }
}

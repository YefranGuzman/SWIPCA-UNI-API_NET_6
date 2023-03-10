using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    [ApiController]
    [Route("api/Clases")]
    public class ClaseController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<Clase>>> Get()
        {
            var clase = new DA_Clase();
            var ListaClase = await clase.ListarClases();

            return (ListaClase);
        }
    }
}

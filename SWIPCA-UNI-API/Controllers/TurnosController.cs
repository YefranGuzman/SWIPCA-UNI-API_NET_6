using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.Controllers;
using SWIPCA_UNI_API.DataAccess;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : Controller
    {
        [HttpGet("Listar_Turnos")]
        public async Task<ActionResult<List<Turno>>> Listar()
        {
            var Dataccess = new DA_Turno();
            var list = await Dataccess.ListarTurnos();
            return list;
        }
    }
}

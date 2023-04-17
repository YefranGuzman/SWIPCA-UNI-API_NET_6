using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.DataAccess;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturasController : Controller
    {
        [HttpGet]
        public async Task <ActionResult<List<Asignatura>>> GetAsignaturas()
        {
            var Asignaturas = new DA_Asignatura();
            var ListaAsignatura = await Asignaturas.ListarAsignaturas();  

            return (ListaAsignatura);
        }
        [HttpPut("{idAsignatura}")]
        public async Task <ActionResult> PutAsignatura(int idAsignatura, [FromBody] Asignatura asignatura)
        {
            var Asignaturas = new DA_Asignatura();
            asignatura.IdAsignatura = idAsignatura;
            await Asignaturas.ActualizarAsignatura(asignatura);
            return Ok(asignatura);
        }
    }
}
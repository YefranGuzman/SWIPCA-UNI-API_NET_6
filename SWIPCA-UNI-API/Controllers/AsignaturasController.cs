using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.DataAccess;

namespace SWIPCA_UNI_API.Controllers
{
    [ApiController]
    [Route("api/Asignatura")]
   
    public class AsignaturasController : Controller
    {
        [HttpGet(Name = "ObtenerListaAsignaturas")]
        public async Task <ActionResult<List<Asignatura>>> Get()
        {
            var Asignaturas = new DA_Asignatura();
            var ListaAsignatura = await Asignaturas.ListarAsignaturas();  

            return (ListaAsignatura);
        }
        
        [HttpPut("{idAsignatura}")]
        public async Task <ActionResult> Put(int idAsignatura, [FromBody] Asignatura asignatura)
        {
            var Asignaturas = new DA_Asignatura();
            asignatura.idAsignatura = idAsignatura;
            await Asignaturas.ActualizarAsignatura(asignatura);
            return Ok(asignatura);
        }

    }

    
}

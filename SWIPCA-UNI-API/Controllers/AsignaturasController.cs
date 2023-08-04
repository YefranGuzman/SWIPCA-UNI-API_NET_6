using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.DataAccess;
using static SWIPCA_UNI_API.DataAccess.DA_Asignatura;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturasController : Controller
    {
        private readonly DA_Asignatura da_asignatura;

        public AsignaturasController(DA_Asignatura asignatura)
        {
            da_asignatura = asignatura; 
        }

        [HttpGet("ListaAsignaturas")]
        public async Task <ActionResult<List<Asignatura>>> GetAsignaturas(int idUsuario)
        {
            var ListaAsignatura = await da_asignatura.ListarAsignaturas(idUsuario); 

            return Ok(ListaAsignatura);
        }
        [HttpPut("ActualizarAsignatura")]
        public async Task <ActionResult> PutAsignatura(int idAsignatura, [FromBody] Asignatura asignatura)
        {
            asignatura.IdAsignatura = idAsignatura;
            await da_asignatura.ActualizarAsignatura(asignatura);
            return Ok(asignatura);
        }
        [HttpGet("listarAsignaturasDepartamento")]
        public async Task <ActionResult<List<AsignaturasDTO>>> ObtenerAsignaturasDepartamento(int Usuario)
        {
            var list = await da_asignatura.AsignaturasDepartamento(Usuario);

            return list;
        }

    }
}
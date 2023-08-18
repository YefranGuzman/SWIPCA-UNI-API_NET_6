using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaAcademicaFcysController : ControllerBase
    {
        private readonly DbCargaAcademicaContext _context;

        public CargaAcademicaFcysController(DbCargaAcademicaContext dbCargaAcademicaContext)
        {
            _context= dbCargaAcademicaContext;     
        }

        // GET: api/<ListarAula>
        [HttpGet("ListarAulas")]
        public IEnumerable<Aula> ListarAulas()
        {
            return _context.Aula;
        }

        // GET: api/<ListarContrato>
        [HttpGet("ListarContrato")]
        public IEnumerable<Contrato> ListarContrato()
        {
            return _context.Contrato;
        }

        // GET: api/<ListarRol>
        [HttpGet("ListarRol")]
        public IEnumerable<Rol> ListarRol()
        {
            return _context.Rol;
        }

        // GET api/<CargaAcademicaFcys>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CargaAcademicaFcys>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CargaAcademicaFcys>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CargaAcademicaFcys>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

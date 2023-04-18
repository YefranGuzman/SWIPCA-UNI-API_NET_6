using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Clase
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();
        public async Task<List<Clase>> ListarClases()
        {
            var Listarclases = await db.Clases.ToListAsync();
            return Listarclases;
        }
    }
}

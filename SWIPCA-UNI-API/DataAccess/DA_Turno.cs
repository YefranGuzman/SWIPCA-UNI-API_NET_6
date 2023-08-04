using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Turno
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<List<Turno>> ListarTurnos()
        {
            var list = await db.Turnos.ToListAsync();

            return list;
        }
    }
}

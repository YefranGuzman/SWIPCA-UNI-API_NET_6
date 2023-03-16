using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Grupo
    {
        DbCargaAcademicaContext db = new();
        public async Task<List<Grupo>> ObtenerGrupo()
        {
            var ListGrupo = await db.Grupos.ToListAsync();
            return ListGrupo;
        }
    }
}

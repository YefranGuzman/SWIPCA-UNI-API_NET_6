using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Disciplina
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<List<string>> ObtenerNombresDisciplinas()
        {
            var ListaDisciplinas = await db.Disciplinas
                                   .Where(p => p.Nombre == null)
                                   .Select(p => p.Nombre)
                                   .ToListAsync();
            return ListaDisciplinas;
        }
    }
}

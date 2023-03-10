using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Aula
    {
        DbCargaAcademicaContext cn = new DbCargaAcademicaContext();
        public async Task<List<Aula>> ListarAula()
        {
            var listAula = await cn.Aulas.ToListAsync();
            return listAula;
        }
    }
}

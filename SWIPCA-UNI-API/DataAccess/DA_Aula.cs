using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Aula
    {
        DbCargaAcademicaContext cn = new DbCargaAcademicaContext();
        public async Task<List<AulaLaboratorio>> ListarAula()
        {
            var listAula = await cn.AulaLaboratorios.ToListAsync();
            return listAula.ToList();
        }
    }
}

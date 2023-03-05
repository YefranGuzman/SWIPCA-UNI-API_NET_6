using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Departamento
    {
        DbCargaAcademicaContext conexion = new DbCargaAcademicaContext();

        public async Task<List<Departamento>> ListarClases()
        {
            return await conexion.Departamentos.ToListAsync();
        }
    }
}

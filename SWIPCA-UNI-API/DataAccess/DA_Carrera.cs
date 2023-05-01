using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Carrera
    {
        DbCargaAcademicaContext conexion = new DbCargaAcademicaContext();

        public async Task<List<Carrera>> ListarCarreras()
        {
            var carreras = await conexion.Carreras.ToListAsync();
            return carreras;
        }

        public async Task<int> ActualizarCarrera(Carrera carrera)
        {
            using (var db = new DbCargaAcademicaContext())
            {
                // Consultar el registro que deseas actualizar.
                var carreraExistente = await conexion.Carreras.FindAsync(carrera.IdCarrera);

                if (carreraExistente != null)
                {
                    // Modificar las propiedades del registro.
                    carreraExistente.Nombre = carrera.Nombre;
                    carreraExistente.IdFacultad = carrera.IdFacultad;

                    // Guardar los cambios en la base de datos.
                    return await db.SaveChangesAsync();
                }

                return 0;
            }
        }
    }
}

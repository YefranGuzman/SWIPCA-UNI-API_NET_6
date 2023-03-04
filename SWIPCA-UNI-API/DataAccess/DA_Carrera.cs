using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Carrera
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();

        public async Task<List<Carrera>> ListarCarreras()
        {
            var ListCarrera = new List<Carrera>();
            using (var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ListarCarrera", sentenciasql))
                {
                    await sentenciasql.OpenAsync();
                    consulta.CommandType = CommandType.StoredProcedure;

                    using (var item = await consulta.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var Carrera = new Carrera();
                            Carrera.IdCarrera = (int)item["idDocente"];
                            Carrera.Nombre = (string)item["HoraFinal"];
                            Carrera.IdFacultad = (int)item["HoraInicio"];
                            ListCarrera.Add(Carrera);
                        }
                    }
                }
                return ListCarrera;
            }
        }

        public async Task<int> ActualizarCarrera(Carrera carrera)
        {
            using (var db = new DbCargaAcademicaContext())
            {
                // Consultar el registro que deseas actualizar.
                var carreraExistente = await db.Carreras.FindAsync(carrera.IdCarrera);

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

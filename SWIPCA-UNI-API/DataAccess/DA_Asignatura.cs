using System.Data;
using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Asignatura
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();
        public async Task<List<Asignatura>> ListarAsignaturas() { 
            var ListAsignatura = new List<Asignatura>();
            using(var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ListarAsignaturas", sentenciasql))
                {
                    await sentenciasql.OpenAsync();
                    consulta.CommandType = CommandType.StoredProcedure;

                    using(var item = await consulta.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var Asignatura = new Asignatura();
                            Asignatura.idAsignatura = (int)item["Identificador"];
                            Asignatura.nombre = (string)item["Nombre"];
                            Asignatura.frecuencia = (int)item["FrecuenciaAsignatura"];
                            ListAsignatura.Add(Asignatura);
                        }
                    }
                }

                return ListAsignatura;
            }
        }
        public async Task ActualizarAsignatura(Asignatura ModelAsignatura) {
            using (var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ActualizarAsignaturas", sentenciasql))
                {
                    consulta.CommandType= CommandType.StoredProcedure;
                    consulta.Parameters.AddWithValue("@idAsignatura", ModelAsignatura.idAsignatura);
                    await sentenciasql.OpenAsync();
                    await consulta.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

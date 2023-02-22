using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Aula
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();
        public async Task<List<Aula>> ListarAsignaturas()
        {
            var ListAula = new List<Aula>();
            using (var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ListarAula", sentenciasql))
                {
                    await sentenciasql.OpenAsync();
                    consulta.CommandType = CommandType.StoredProcedure;

                    using (var item = await consulta.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var Aulas = new Aula();
                            Aulas.idAula = (int)item["Identificador"];
                            Aulas.nombre = (string)item["Nombre"];
                            Aulas.NumeroAula = (string)item["NumeroAula"];
                            Aulas.idFacultad = (int)item["idFacultad"];
                            ListAula.Add(Aulas);
                        }
                    }
                }

                return ListAula;
            }
        }
    }
}

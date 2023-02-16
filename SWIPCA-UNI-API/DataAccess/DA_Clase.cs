using System.Data;
using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Clase
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();

        public async Task<List<Clase>> ListarClases()
        {
            var ListClase = new List<Clase>();
            using (var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ListarClases", sentenciasql))
                {
                    await sentenciasql.OpenAsync();
                    consulta.CommandType = CommandType.StoredProcedure;

                    using (var item = await consulta.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync()) { 
                            var clase = new Clase();
                            clase.IdDocente = (int)item["idDocente"];
                            clase.Horainicio = (DateTime)item["HoraFinal"];
                            clase.Horafinal = (DateTime)item["HoraInicio"];
                            clase.Dia = (string)item["Dia"];
                            ListClase.Add(clase);
                        }
                    }
                }
                return ListClase;
            }
        }
    }
}

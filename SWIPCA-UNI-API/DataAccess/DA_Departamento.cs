using Microsoft.Data.SqlClient;
using SWIPCA_UNI_API.Models;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Departamento
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();

        public async Task<List<Departamento>> ListarClases()
        {
            var ListDptamento = new List<Departamento>();
            using (var sentenciasql = new SqlConnection(conexion.ConexionSQL()))
            {
                using (var consulta = new SqlCommand("sp_ListarDepartamentos", sentenciasql))
                {
                    await sentenciasql.OpenAsync();
                    consulta.CommandType = CommandType.StoredProcedure;

                    using (var item = await consulta.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var departamento = new Departamento();
                            departamento.IdFacultad = (int)item["idDepartamento"];
                            departamento.IdDepartamento = (int)item["HoraFinal"];
                            departamento.Nombre = (string)item["HoraInicio"];
                            departamento.IdUsuario = (int)item["Dia"];
                            ListDptamento.Add(departamento);
                        }
                    }
                }
                return ListDptamento;
            }
        }
    }
}

using System.Data;
using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Asignatura
    {
        DB_Carga_Academica conexion = new DB_Carga_Academica();
        DbCargaAcademicaContext cn = new DbCargaAcademicaContext();
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
                            Asignatura.IdAsignatura = (int)item["Identificador"];
                            Asignatura.Nombre = (string)item["Nombre"];
                            Asignatura.Frecuencia = (int)item["FrecuenciaAsignatura"];
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
                    consulta.Parameters.AddWithValue("@idAsignatura", ModelAsignatura.IdAsignatura);
                    await sentenciasql.OpenAsync();
                    await consulta.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<string>> ObtenerAsignaturas(int IdDocente)
        {
            var ListaAsignaturas = await (from AA in cn.Asignaturas
                                          join DP in cn.Departamentos
                                          on AA.IdDepartamento equals DP.IdDepartamento
                                          join FTL in cn.Facultads
                                          on DP.IdFacultad equals FTL.IdFacultad
                                          join DC in cn.Docentes
                                          on DP.IdDepartamento equals DC.IdDepartamento
                                          join DIS in cn.Disponibilidads
                                          on DC.IdDocente equals DIS.IdDocente
                                          join CS in cn.Clases
                                          on AA.IdAsignatura equals CS.IdAsignatura
                                          join CR in cn.Carreras
                                          on FTL.IdFacultad equals CR.IdFacultad
                                          join GT in cn.Grupos
                                          on CR.IdCarrera equals GT.IdCarrera
                                          join AU in cn.Aulas
                                          on FTL.IdFacultad equals AU.IdFacultad
                                          where DC.IdDocente == IdDocente
                                          select AA.Nombre + " " + CS.Dia + " " + GT.Nombre + " " + AU.NumeroAula + " " + CS.HoraInicio
                ).ToListAsync();
            return ListaAsignaturas;
        } 
    }
}

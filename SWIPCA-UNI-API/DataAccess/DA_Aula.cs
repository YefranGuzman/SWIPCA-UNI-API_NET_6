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
        public async Task<List<AulaLaboratorio>> ListarAulaPorFacultad(int IdUsuario)
        {
            var aulasAsignadas = await (from aula in cn.AulaLaboratorios
                               join facultad in cn.Facultads on aula.IdFacultad equals facultad.IdFacultad
                               join departamento in cn.Departamentos on facultad.IdFacultad equals departamento.IdFacultad
                               join docente in cn.Docentes on departamento.IdDepartamento equals docente.IdDepartamento
                               join usuario in cn.Usuarios on docente.IdUsuario equals usuario.IdUsuario
                               where docente.IdUsuario == IdUsuario
                               select aula
                              ).ToListAsync();

            var aulasNoAsignadas = await (
                                    from aula in cn.AulaLaboratorios
                                    where !aulasAsignadas.Contains(aula)
                                    select aula
                                      ).ToListAsync();

            if (aulasAsignadas.Any())
            {
                return aulasAsignadas;
            }
            else
            { 
                return aulasNoAsignadas;
            }

        }
    }
}

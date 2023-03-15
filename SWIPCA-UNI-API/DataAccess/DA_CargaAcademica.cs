using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_CargaAcademica
    {
        DbCargaAcademicaContext db = new();

        public async Task<List<string>> ObtenerCargaAcademicaDocente(int IdDocente, int IdDepartameto)
        {
            var result = new List<string>();

            var idCargaAcademica = await db.CargaAcademicas
                .Join(db.Docentes,
                jefe => jefe.IdJefe,
                docente => docente.IdDocente,
                (jefe, docente) => new
                {
                    CargaAcademicas = jefe,
                    Docentes = docente
                })
                .Where(x => x.Docentes.IdDocente == IdDocente)
                .Select(x => x.CargaAcademicas.IdCaHo)
                .FirstOrDefaultAsync();

            if (idCargaAcademica != null && IdDepartameto != null)
            {
                var cargaTurno = await (from A in db.CargaAcademicas
                                         join B in db.Grupos
                                         on A.IdGrupo equals B.IdGrupo
                                         where A.IdCaHo == idCargaAcademica
                                         select B.Turno).ToListAsync();

                result.AddRange((IEnumerable<string>)cargaTurno);

                var cargaClases = await (from A in db.CargaAcademicas
                                         join B in db.Clases
                                         on A.IdClase equals B.IdClase
                                         join C in db.Asignaturas
                                         on B.IdAsignatura equals C.IdAsignatura
                                         where A.IdCaHo == idCargaAcademica
                                         select C.Nombre).ToListAsync();

                result.AddRange(cargaClases);

                var cargaAulas = await (from A in db.AulaLaboratorios
                                        join B in db.Facultads
                                        on A.IdFacultad equals B.IdFacultad
                                        join C in db.Departamentos
                                        on B.IdFacultad equals C.IdFacultad
                                        join D in db.Docentes
                                        on C.IdDepartamento equals D.IdDepartamento
                                        where C.IdDepartamento == IdDepartameto && D.IdDocente == IdDocente
                                        select A.IdAuLa
                                        ).ToListAsync();

                var cargaGrupos = await (from A in db.CargaAcademicas
                                        join B in db.Grupos
                                        on A.IdGrupo equals B.IdGrupo
                                        where A.IdCaHo == idCargaAcademica
                                        select B.Nombre).ToListAsync();

                result.AddRange(cargaGrupos);

                var cargaFrecuencia = await (from A in db.CargaAcademicas
                                         join B in db.Clases
                                         on A.IdClase equals B.IdClase
                                         where A.IdCaHo == idCargaAcademica
                                         select B.Dia).ToListAsync();

                result.AddRange(cargaFrecuencia);

            }
            return result;
        }
    }
}

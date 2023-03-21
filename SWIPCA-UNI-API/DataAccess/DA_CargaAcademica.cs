using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_CargaAcademica
    {
        DbCargaAcademicaContext db = new();

        public async Task<List<string>> ObtenerCargaAcademicaDocente(int IdDocente)
        {
            var CargaAcademica1 = await (from carga in db.CargaAcademicas
                                        join docente in db.Docentes
                                        on carga.IdDocente equals docente.IdDocente
                                        join clase in db.Clases
                                        on carga.IdClase equals clase.IdClase
                                        join grupo in db.Grupos
                                        on carga.IdGrupo equals grupo.IdGrupo
                                        join asignatura in db.Asignaturas
                                        on clase.IdAsignatura equals asignatura.IdAsignatura
                                        join carrera in db.Carreras
                                        on carga.IdCarrera equals carrera.IdCarrera
                                        join facultad in db.Facultads
                                        on carrera.IdFacultad equals facultad.IdFacultad
                                        join Aula_Lab in db.AulaLaboratorios
                                        on facultad.IdFacultad equals Aula_Lab.IdFacultad
                                        where carga.IdDocente == IdDocente && carga.Estado == 0
                                        select $"{carga.IdCaHo}{asignatura.Nombre}{Aula_Lab.Nombre}{grupo.Nombre}"
                                        ).ToListAsync();

            var CargaAcademica2 = await (from carga in db.CargaAcademicas
                                   join docente in db.Docentes
                                   on carga.IdDocente equals docente.IdDocente
                                   join clase in db.Clases
                                   on carga.IdClase equals clase.IdClase
                                   join grupo in db.Grupos
                                   on carga.IdGrupo equals grupo.IdGrupo
                                   join asignatura in db.Asignaturas
                                   on clase.IdAsignatura equals asignatura.IdAsignatura
                                   join carrera in db.Carreras
                                   on carga.IdCarrera equals carrera.IdCarrera
                                   join facultad in db.Facultads
                                   on carrera.IdFacultad equals facultad.IdFacultad
                                   join Aula_Lab in db.AulaLaboratorios
                                   on facultad.IdFacultad equals Aula_Lab.IdFacultad
                                   where carga.IdDocente == IdDocente && carga.Estado == 0
                                   select $"{carga.Observacion}{asignatura.Frecuencia}").ToListAsync();

            var CargaAcademica = CargaAcademica1.Concat(CargaAcademica2).ToList();

            return CargaAcademica;
        }
    }
}

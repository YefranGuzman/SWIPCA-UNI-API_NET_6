using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_CargaAcademica
    {
        DbCargaAcademicaContext db = new();

        public async Task AgregarCargaAcademica(CargaAcademica cargaAcademica)
        {
            if (db.CargaAcademicas.Any(c => c.IdCarrera == cargaAcademica.IdCarrera && c.IdClase == cargaAcademica.IdClase && c.IdGrupo == cargaAcademica.IdGrupo && c.IdDocente == cargaAcademica.IdDocente))
            {
                throw new Exception("Ya existe una carga académica con los mismos valores de IdCarrera, IdClase, IdGrupo e IdDocente.");
            }

            db.CargaAcademicas.Add(cargaAcademica);
            await db.SaveChangesAsync();
        }
        public async Task CambiarEstadoCargaAcademicaAprobada(int idCargaAcademica)
        {
            var cargaAcademica = await db.CargaAcademicas.FindAsync(idCargaAcademica);

            if (cargaAcademica == null)
            {
                throw new ArgumentException("La carga académica no existe");
            }

            if (cargaAcademica.Estado != 0)
            {
                throw new InvalidOperationException("La carga académica ya ha sido aprobada o rechazada");
            }

            cargaAcademica.Estado = 1;
            await db.SaveChangesAsync();
        }
        public async Task CambiarEstadoCargaAcademicaDenegada(int idCargaAcademica)
        {
            var cargaAcademica = await db.CargaAcademicas.FindAsync(idCargaAcademica);

            if (cargaAcademica == null)
            {
                throw new ArgumentException("La carga académica no existe");
            }

            if (cargaAcademica.Estado != 0)
            {
                throw new InvalidOperationException("La carga académica ya ha sido aprobada o rechazada");
            }

            cargaAcademica.Estado = 2;
            await db.SaveChangesAsync();
        }
        public async Task<List<CargaAcademicaDTO>> ObtenerCargaAcademicaDocente(int IdDocente, int IdTurno)
        {
            IdTurno = 0;//0 para Matutino, 1 para Vespertino y 2 para Nocturno

            var PRECargaAcademica = await (from carga in db.CargaAcademicas
                                           join docente in db.Docentes on carga.IdDocente equals docente.IdDocente
                                           join clase in db.Clases on carga.IdClase equals clase.IdClase
                                           join grupo in db.Grupos on carga.IdGrupo equals grupo.IdGrupo
                                           join turno in db.Turnos on grupo.IdTurno equals turno.IdTurno
                                           join asignatura in db.Asignaturas on clase.IdAsignatura equals asignatura.IdAsignatura
                                           join carrera in db.Carreras on carga.IdCarrera equals carrera.IdCarrera
                                           join facultad in db.Facultads on carrera.IdFacultad equals facultad.IdFacultad
                                           join Aula_Lab in db.AulaLaboratorios on facultad.IdFacultad equals Aula_Lab.IdFacultad
                                           where carga.IdDocente == IdDocente && carga.Estado == 0 && turno.IdTurno == IdTurno
                                           group new { asignatura, turno } by new { carga.IdCaHo, grupo.Nombre, carga.Observacion, Aula = asignatura.Nombre, asignatura.Frecuencia } into g
                                           select new CargaAcademicaDTO
                                           {
                                               IdCarga = g.Key.IdCaHo,
                                               Asignatura = g.Key.Nombre,
                                               Aula = g.Key.Aula,
                                               Grupo = g.Key.Nombre,
                                               Horario = g.ToDictionary(x => x.turno.Dia, x => x.turno.HoraInicio),
                                               Frecuencia = g.Key.Frecuencia,
                                               Observacion = g.Key.Observacion
                                           }).ToListAsync();
            return PRECargaAcademica;
        }

        public async Task GuardarCargaAcademica(CargaAcademica cargaAcademica)
        {
            if (cargaAcademica.IdCarrera == 0 ||
                cargaAcademica.IdClase == 0 ||
                cargaAcademica.IdGrupo == 0 ||
                cargaAcademica.IdDocente == 0 ||
                cargaAcademica.IdJefe == 0)
            {
                throw new ArgumentException("Debe ingresar todos los datos requeridos.");
            }
            await db.CargaAcademicas.AddAsync(cargaAcademica);
            await db.SaveChangesAsync();
        }
        public class CargaAcademicaDTO
        {
            public int IdCarga { get; set; }
            public string Asignatura { get; set; }
            public string Aula { get; set; }
            public string Grupo { get; set; }
            public Dictionary<string, TimeSpan> Horario { get; set; }
            public int Frecuencia { get; set; }
            public string Observacion { get; set; }
            public string ObservacionValidada => string.IsNullOrEmpty(Observacion) ? "Sin observaciones" : Observacion;
        }
    }
}

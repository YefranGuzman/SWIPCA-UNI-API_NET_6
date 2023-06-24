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

            var grupo = await db.Grupos.FirstOrDefaultAsync(g => g.IdGrupo == cargaAcademica.IdGrupo);
            if (grupo == null)
            {
                throw new Exception($"No se encontró el grupo para el IdGrupo: {cargaAcademica.IdGrupo}");
            }

            var clase = await db.Clases.FirstOrDefaultAsync(c => c.IdClase == cargaAcademica.IdClase);
            if (clase == null)
            {
                throw new Exception($"No se encontró la clase para el IdClase: {cargaAcademica.IdClase}");
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
        public async Task<List<CargaAcademicaDTO>> ObtenerCargaAcademicaDocente(int IdUsuarioLogin, int idUsuarioObtener, string nombreturno)
        {
            var obtenerturnos = await (from a in db.CargaAcademicas
                                       join b in db.Grupos
                                       on a.IdGrupo equals b.IdCarrera
                                       join c in db.Turnos
                                       on b.IdTurno equals c.IdTurno
                                       where c.Nombre == nombreturno
                                       select a.IdCaHo
                                       ).ToListAsync();

            var jefeACargo = await (from a in db.CargaAcademicas
                                    join b in db.Departamentos
                                    on a.IdJefe equals b.Jefe
                                    where b.Jefe == IdUsuarioLogin
                                    select a.IdJefe).FirstOrDefaultAsync();

            var obtenerlistaAsignaturas = await(from a in db.Docentes
                                                join b in db.Clases
                                                on a.IdDocente equals b.IdDocente
                                                select b.IdClase).ToListAsync();

            var cargaAcademica = await (from a in db.CargaAcademicas
                                        join b in db.Docentes
                                        on a.IdDocente equals b.IdDocente
                                        join c in db.Grupos
                                        on a.IdGrupo equals c.IdGrupo
                                        join d in db.Clases
                                        on a.IdClase equals d.IdClase
                                        join e in db.Asignaturas
                                        on d.IdAsignatura equals e.IdAsignatura
                                        where b.IdUsuario == idUsuarioObtener && a.IdJefe == jefeACargo && obtenerturnos.Contains(c.IdGrupo) && obtenerlistaAsignaturas.Contains(a.IdClase) && a.Estado == 0
                                        select new CargaAcademicaDTO { 
                                            IdCarga = a.IdCaHo,
                                            Asignatura = e.Nombre,
                                            Grupo = c.Nombre,
                                            Horario = d.HoraInicio + " a " + d.HoraFinal,
                                            Frecuencia = e.Frecuencia
                                        }).ToListAsync();
                                
            return cargaAcademica;                    
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
            public string Horario { get; set; }
            public int Frecuencia { get; set; }
            public string Observacion { get; set; }
            public string ObservacionValidada => string.IsNullOrEmpty(Observacion) ? "Sin observaciones" : Observacion;
        }
    }
}

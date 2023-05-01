using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Controllers;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Disponibilidad
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<string> GuardarDisponibilidadDocente(int IdDocente, string Observacion, int Periodo, string Evidencia, int Estado, int TipoJustificacion)
        {
            if (IdDocente == 0)
            {
                return "El Id del docente no puede venir vacio";
            }

            var save = new Disponibilidad
            {
                IdDocente = IdDocente,
                Observacíon = Observacion,
                Fecha = DateTime.Now,
                Periodicidad = Periodo,
                Evidencia = Evidencia,
                Estado = Estado,
                TipoJustificación = TipoJustificacion
            };

            db.Disponibilidads.Add(save);
            await db.SaveChangesAsync();

            return "Disponibilidad guardada exitosamente";
        }
        public async Task<List<Disponibilidad>> ObtenerDisponibilidadesPorEstado(int idDocente)
        {
            int Estado = 0;
            var disponibilidades = await db.Disponibilidads
                .Include(d => d.IdDocenteNavigation)
                .Where(d => d.IdDocente == idDocente && d.Estado.Equals(Estado))
                .ToListAsync();
            if(disponibilidades.Count == 0)
            {
                return new List<Disponibilidad>() { new Disponibilidad() { Observacíon = "No se encontraron disponibilidades activas." } };
            }

            return disponibilidades;
        }
        public async Task<List<Disponibilidad>> ObtenerDisponibilidadesTodosDocentesPorDepartamento(int idDocente)
        {
            int Estado = 1;

            var DepartamentoJefe = await (from dpto in db.Departamentos
                                          join doc in db.Docentes
                                          on dpto.Jefe equals doc.IdDocente
                                          where doc.IdDocente.Equals(idDocente)
                                          select doc.IdDocente).FirstOrDefaultAsync();

            var CargaAcademica = await (from C_Academica in db.CargaAcademicas
                                        join doc in db.Docentes
                                        on C_Academica.IdDocente equals doc.IdDocente
                                        join Dpto in db.Departamentos
                                        on C_Academica.IdJefe equals Dpto.Jefe
                                        where C_Academica.IdJefe == DepartamentoJefe
                                        select C_Academica.IdDocente).ToListAsync();

            var disponibilidades = await db.Disponibilidads
                .Include(d => d.IdDocenteNavigation)
                .Where(d => CargaAcademica.Contains(d.IdDocente) && d.Estado.Equals(Estado))
                .ToListAsync();

            if (disponibilidades.Count == 1)
            {
                throw new Exception("Todos los docentes tienen disponibilidad de Tiempo");
            }

            return disponibilidades;
        }
        public async Task<List<SolicitudDisponibilidadDTO>> AprobarDisponibilidades(int idJefe)
        {
            var docentesdepartamentos = await (from docente in db.Docentes
                                               join departamento in db.Departamentos
                                               on docente.IdDocente equals departamento.Jefe
                                               where departamento.Jefe == idJefe
                                               select docente.IdDepartamento).FirstOrDefaultAsync();

            var docentesdispobilidad = await (from docente in db.Docentes
                                              join disponibilidad in db.Disponibilidads
                                              on docente.IdDocente equals disponibilidad.IdDocente
                                              join departamento in db.Departamentos
                                              on docente.IdDepartamento equals departamento.IdDepartamento
                                              join usuario in db.Usuarios
                                              on docente.IdUsuario equals usuario.IdUsuario
                                              where docente.IdDepartamento == docentesdepartamentos
                                              select new SolicitudDisponibilidadDTO
                                              {
                                                 Docente = usuario.PrimerNombre + " " + usuario.SegundoNombre + " " + usuario.PrimerApellido + " " + usuario.SegundoNombre,
                                                 Fecha = disponibilidad.Fecha,
                                                 TipoJustificacion = disponibilidad.TipoJustificación,
                                                 Estado = disponibilidad.Estado,
                                                 Periodo = disponibilidad.Periodicidad,
                                                 Razon = disponibilidad.Observacíon,
                                                 Evidencia = disponibilidad.Evidencia
                                              }).ToListAsync();
            return docentesdispobilidad;
        }
        public async Task<bool> ActualizarEstadoSolicitudDisponibilidad(int idSolicitud, int nuevoEstado)
        {
            var solicitud = await db.Disponibilidads.FindAsync(idSolicitud);

            if (solicitud == null)
            {
                throw new ArgumentException("La solicitud de disponibilidad no existe");
            }

            solicitud.Estado = nuevoEstado;
            await db.SaveChangesAsync();

            return true;
        }

        public class SolicitudDisponibilidadDTO
        {
            public string Docente { get; set; }
            public DateTime Fecha { get; set; }
            public int TipoJustificacion { get; set; }
            public int Estado { get; set; }
            public int Periodo { get; set; }
            public string Razon { get; set; }
            public string Evidencia { get; set;}
        }
    }
}

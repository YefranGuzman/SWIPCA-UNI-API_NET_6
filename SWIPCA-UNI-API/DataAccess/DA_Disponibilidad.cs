using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Controllers;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Disponibilidad
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();
        public async Task<string> GuardarDisponibilidadDocente(int IdUsuario, string Observacion, int Periodo, string Evidencia, int Estado, int TipoJustificacion)
        {
            var obtenerdocente = await db.Docentes.FirstOrDefaultAsync(a => a.IdUsuario == IdUsuario);

            if (IdUsuario == 0)
            {
                return "El Id del usuario no puede venir vacio";
            }

            if (obtenerdocente.IdDocente != null)
            {
                var save = new Disponibilidad
                {
                    IdDocente = obtenerdocente.IdDocente,
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
            return "El Id del docente no puede venir vacio";
        }
        public async Task<List<SolicitudDisponibilidadDTO>> ObtenerDisponibilidades(int idUsuario)
        {
         
            var rolUsuario = await (from a in db.Usuarios
                                    where a.IdUsuario == idUsuario
                                    select a.TipoRol).FirstOrDefaultAsync();

            if (rolUsuario == 0)
            {
                throw new Exception("Este usuario no cuenta con rol asignado");
            }

            if(rolUsuario == 2)
            {
                var obtenerdocente = await db.Docentes.FirstOrDefaultAsync(a => a.IdUsuario == idUsuario);

                if (obtenerdocente == null)
                {
                    throw new Exception("Este usuario no cumple con ningun docente");
                }

                var disponibilidades = await (from a in db.Disponibilidads
                                              join b in db.Docentes
                                              on a.IdDocente equals b.IdDocente
                                              join c in db.Usuarios
                                              on b.IdUsuario equals c.IdUsuario
                                              where b.IdUsuario == obtenerdocente.IdUsuario
                                              select new SolicitudDisponibilidadDTO
                                              {
                                                  Docente = c.UserName,
                                                  Fecha = a.Fecha,
                                                  TipoJustificacion = a.TipoJustificación,
                                                  Estado = a.Estado,
                                                  Periodo = a.Periodicidad,
                                                  Razon = a.Observacíon,
                                                  Evidencia = a.Evidencia
                                              }).ToListAsync();
                                               

                if (disponibilidades.Count == 0)
                {
                    return new List<SolicitudDisponibilidadDTO>() { new SolicitudDisponibilidadDTO() { Razon = "No se encontraron disponibilidades activas." } };
                }

                return disponibilidades;

            }if (rolUsuario == 3)
            {
                var departamentoJefe = await db.Departamentos
                                        .FirstOrDefaultAsync(a => a.Jefe == idUsuario);

                if (departamentoJefe == null)
                {
                    throw new Exception("Este usuario no es jefe de departamento");
                }

                var disponibilidades = await (from a in db.Disponibilidads
                                              join b in db.Docentes
                                              on a.IdDocente equals b.IdDocente
                                              join c in db.Usuarios
                                              on b.IdUsuario equals c.IdUsuario
                                              join d in db.Departamentos
                                              on b.IdDepartamento equals d.IdDepartamento
                                              where d.IdDepartamento == departamentoJefe.IdDepartamento
                                              select new SolicitudDisponibilidadDTO
                                              {
                                                  Docente = c.UserName,
                                                  Fecha = a.Fecha,
                                                  TipoJustificacion = a.TipoJustificación,
                                                  Estado = a.Estado,
                                                  Periodo = a.Periodicidad,
                                                  Razon = a.Observacíon,
                                                  Evidencia = a.Evidencia
                                              }).ToListAsync();


                if (disponibilidades.Count == 0)
                {
                    // Puedes devolver una lista con una disponibilidad ficticia indicando que no se encontraron disponibilidades.
                    return new List<SolicitudDisponibilidadDTO>() { new SolicitudDisponibilidadDTO() { Razon = "No se encontraron disponibilidades activas." } };
                }

                return disponibilidades;
            }

            throw new Exception("Disponibilidad no corresponde");
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
        public async Task<bool> ActualizarEstadoSolicitudDisponibilidad(int IdUsuario, int IdSolicitud ,int NuevoEstado)
        {


            var solicitud = await db.Disponibilidads.FirstAsync(a => a.IdDisponibilidad == IdSolicitud);

            if (solicitud == null)
            {
                throw new ArgumentException("La solicitud de disponibilidad no existe");
            }

            solicitud.Estado = NuevoEstado;
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
            public string? Evidencia { get; set;}
        }
    }
}

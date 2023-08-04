using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using SWIPCA_UNI_API.Controllers;
using System.Diagnostics;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Clase
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext(); 
        public async Task<List<Clase>> ListarClases()   
        {
            var Listarclases = await db.Clases.ToListAsync();

            return Listarclases;            
        }
        public async Task<List<AgendaDTO>> ObtenerAgenda(int idUsuario)
        {

            var agenda = await (
                from a in db.Clases
                join b in db.Docentes on a.IdDocente equals b.IdDocente
                join c in db.Asignaturas on a.IdAsignatura equals c.IdAsignatura
                join d in db.CargaAcademicas on b.IdDocente equals d.IdDocente
                join e in db.Grupos on d.IdGrupo equals e.IdGrupo
                where b.IdUsuario == idUsuario && d.Estado == 0
                select new AgendaDTO
                {
                    idClase = a.IdClase,
                    Dia = a.Dia,
                    Asignatura = c.Nombre,
                    Grupo = e.Nombre,
                    Hora = a.HoraInicio
                }
            ).ToListAsync();

            return agenda;
        }
        public async Task<ActionResult<Clase>> AgregarClase(int idAsignatura, int idDocente, TimeSpan HoraInicio, TimeSpan HoraFinal, string Dia)
        {
            var CreacionClase = new Clase
            {
                IdAsignatura = idAsignatura,
                IdDocente = idDocente,
                HoraInicio = HoraInicio,
                HoraFinal = HoraFinal,
                Dia = Dia
            };

            db.Clases.Add(CreacionClase);
            await db.SaveChangesAsync();

            // Aquí obtienes la carrera y el grupo correspondiente, reemplaza estos valores con los adecuados
            var carrera = await db.Pensums.FirstAsync(a => a.IdAsignatura == idAsignatura);
            var grupo = await db.Grupos.FirstAsync(a => a.IdCarrera == carrera.IdCarrera);

            var horarioClase = new Horario
            {
                IdClase = CreacionClase.IdClase,
                IdCarrera = carrera.IdCarrera,
                IdGrupo = grupo.IdGrupo,
                // Agrega otros campos necesarios para el horario de clase
            };

            db.Horarios.Add(horarioClase);
            await db.SaveChangesAsync();

            return CreacionClase;
        }


        private async Task<ActionResult<Clase>> GetClase(int id)
        {
            var clase = await db.Clases.FindAsync(id);

            if (clase == null)
            {
                throw new Exception("No encontrado");
            }
            return clase;
        }

        public class AgendaDTO
    {
        public int idClase { get; set; }
        public string Dia { get; set; }
        public string Asignatura { get; set; }
        public string Grupo { get; set; }
            public TimeSpan Hora { get; set; }
        }
    }
}

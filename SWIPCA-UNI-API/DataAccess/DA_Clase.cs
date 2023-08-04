using System.Data;
using SWIPCA_UNI_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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

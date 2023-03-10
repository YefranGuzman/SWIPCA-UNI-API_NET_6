using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Docentes
    {
   
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<List<string>> ObtenerDocentes(int idDepartamento, int idFacultad)
        {
            var listaDocentes = await (from a in db.Docentes
                                       join b in db.Departamentos
                                       on a.IdDepartamento equals b.IdDepartamento
                                       join c in db.Usuarios
                                       on a.IdUsuario equals c.IdUsuario
                                       join f in db.Facultads
                                       on b.IdFacultad equals f.IdFacultad
                                       where b.IdDepartamento == idDepartamento && b.IdFacultad == idFacultad
                                       select c.PrimerNombre + " " + c.SegundoNombre + " " + c.PrimerApellido + " " + c.SegundoApellido).ToListAsync();
            return listaDocentes;
        }

        public async Task<List<string>> ObtenerDisponibilidadDocente(int idDocente)
        {
            var listarDisponibilidad = await (from a in db.Disponibilidads
                                              join b in db.Docentes
                                              on a.IdDocente equals b.IdDocente
                                              where a.IdDocente == idDocente
                                              select a.Fecha + " " + a.TipoJustificación + " " + a.Periodicidad + " " + a.Observacíon + " " + a.Evidencia).ToListAsync();
            return listarDisponibilidad;
        }
        public async Task<List<string>> ObtenerCargaDocentesLaboral(int IdDocente)
        {
            var ListaOCB = await (from DC in db.Docentes
                                          join CS in db.Clases
                                          on DC.IdDocente equals CS.IdDocente
                                          join AA in db.Asignaturas
                                          on CS.IdAsignatura equals AA.IdAsignatura
                                          join HS in db.Horarios
                                          on CS.IdClase equals HS.IdClase
                                          join GT in db.Grupos
                                          on HS.IdGrupo equals GT.IdGrupo
                                          join DP in db.Departamentos
                                          on DC.IdDepartamento equals DP.IdDepartamento
                                          join FT in db.Facultads
                                          on DP.IdFacultad equals FT.IdFacultad
                                          join AU in db.AulaLaboratorios
                                          on FT.IdFacultad equals AU.IdFacultad
                                          where DC.IdDocente == IdDocente && CS.Dia == DateTime.Today.ToString()
                                          select AA.Nombre + " " + CS.Dia + " " + GT.Nombre + " " + AU.Nombre + " " + CS.HoraInicio
                ).ToListAsync();
            return ListaOCB;
        }

        public async Task<List<string>> ObtenerAgendaDocente(int IdDocente)
        {
            var AgendaDocenteClases = await (from DC in db.Docentes
                                             join CS in db.Clases
                                             on DC.IdDocente equals CS.IdDocente
                                             join AA in db.Asignaturas
                                             on CS.IdAsignatura equals AA.IdAsignatura
                                             join HS in db.Horarios
                                             on CS.IdClase equals HS.IdClase
                                             join GT in db.Grupos
                                             on HS.IdGrupo equals GT.IdGrupo
                                             join DP in db.Departamentos
                                             on DC.IdDepartamento equals DP.IdDepartamento
                                             join FT in db.Facultads
                                             on DP.IdFacultad equals FT.IdFacultad
                                             join AU in db.AulaLaboratorios
                                             on FT.IdFacultad equals AU.IdFacultad
                                             join DIS in db.Disponibilidads
                                             on DC.IdDocente equals DIS.IdDocente
                                             where DC.IdDocente == IdDocente
                                             select $"{DIS.Fecha}{DIS.Observacíon}"
                                        ).ToListAsync();
            if (AgendaDocenteClases.Any())
            {
                return AgendaDocenteClases.ToList();
            }
            else
            {
                return new List<string>() { "No hay fechas u observaciones en la agenda del docente." };
            }
        }
        
    }
}

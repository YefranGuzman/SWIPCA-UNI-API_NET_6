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
    /*
     El código define una clase DA_Docentes que tiene cuatro métodos asincrónicos 
     para interactuar con una base de datos a través de Entity Framework Core.

     Los cuatro métodos son ObtenerDocentes, ObtenerDisponibilidadDocente, ObtenerCargaDocentesLaboral 
     y ObtenerAgendaDocente. Cada uno de estos métodos recibe uno o varios parámetros y devuelve una lista de cadenas de texto.

    Cada método usa una o varias consultas LINQ para obtener datos de diferentes tablas de la base de datos, como 
    Docentes, Departamentos, Usuarios, Disponibilidads, Clases, Asignaturas, Horarios, Grupos, Facultads y AulaLaboratorios.
    Las consultas también incluyen cláusulas WHERE para filtrar los resultados según los parámetros proporcionados.

    Una vez obtenidos los resultados de cada consulta, se concatenan las cadenas necesarias y se devuelven en una lista 
    que se retorna como resultado de la función. En caso de no haber resultados, se devuelve una lista vacía o una lista 
    con un único elemento que indica que no hay resultados.
     */
}

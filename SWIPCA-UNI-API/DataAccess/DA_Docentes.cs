using Microsoft.EntityFrameworkCore;
using System.Linq;
using SWIPCA_UNI_API.Models;
using SWIPCA_UNI_API.Controllers;
using System.Runtime.Intrinsics.Arm;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Docentes
    {
   
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<List<DocenteDTO>> ObtenerDocentes(int idUsuario)
        {
            var obtenerDepartamento =  await (from a in db.Departamentos
                                                     join b in db.Usuarios
                                                     on a.Jefe equals b.IdUsuario
                                                     where idUsuario == a.Jefe
                                                     select new Departamento
                                                     {
                                                         IdDepartamento = a.IdDepartamento,
                                                         Jefe = a.Jefe
                                                     }).FirstOrDefaultAsync();

            if(obtenerDepartamento == null)
            {
                throw new Exception("Este usuario no es jefe de departamento");
            }
            else
            {
                var usuariosPorDepartamento = await (from a in db.Departamentos
                                                     join b in db.Docentes
                                                     on a.IdDepartamento equals b.IdDepartamento
                                                     join c in db.Usuarios 
                                                     on b.IdUsuario equals c.IdUsuario
                                                     where a.IdDepartamento == obtenerDepartamento.IdDepartamento
                                                     select new DocenteDTO
                                                     {
                                                         idDocente = b.IdDocente,
                                                         NombreDocente = c.UserName
                                                     })
                                                 .ToListAsync();

                return usuariosPorDepartamento;
            }
        }

        public async Task<List<DisponibilidadDTO>> ObtenerDisponibilidadDocente(int idUsuario)
        {
            var IdDocente = await (from a in db.Usuarios
                                   join b in db.Docentes
                                   on a.IdUsuario equals b.IdUsuario
                                   where a.IdUsuario == idUsuario
                                   select b.IdDocente).FirstOrDefaultAsync();

            var disponibilidadList = await (from a in db.Disponibilidads
                                            where a.IdDocente == IdDocente
                                            select new DisponibilidadDTO
                                            {
                                                Fecha = a.Fecha,
                                                TipoJustificación = a.TipoJustificación,
                                                Periodicidad = a.Periodicidad,
                                                Observación = a.Observacíon,
                                                Evidencia = a.Evidencia
                                            }).ToListAsync();

            return disponibilidadList;
        }

        public async Task<List<Disponibilidad2DTO>> ObtenerAgendaDocente(int idUsuario)
        {
            var AgendaDocenteClases = await (from DC in db.Docentes
                                             join DP in db.Disponibilidads
                                             on DC.IdDocente equals DP.IdDocente
                                             where DC.IdDocente == idUsuario && DP.Fecha == DateTime.Today
                                             select new Disponibilidad2DTO
                                             {
                                                 Fecha = DP.Fecha,
                                                 Observacion = DP.Observacíon
                                             }
                                        ).ToListAsync();

                return AgendaDocenteClases.ToList();

        }
        public class DisponibilidadDTO
        {
            public DateTime Fecha { get; set; }
            public int TipoJustificación { get; set; }
            public int Periodicidad { get; set; }
            public string? Observación { get; set; }
            public string? Evidencia { get; set; }
        }
        public class Disponibilidad2DTO
        {
            public DateTime Fecha { get; set;}
            public string? Observacion { get; set;}
        }
        public class DocenteDTO
        {
            public int idDocente { get; set; }
            public string NombreDocente { get;set; }
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

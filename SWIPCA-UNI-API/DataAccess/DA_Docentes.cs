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
                                       where a.Estado == 1 && b.IdDepartamento == idDepartamento && b.IdFacultad == idFacultad
                                       select c.PrimerNombre + " " + c.SegundoNombre + " " + c.PrimerApellido + " " + c.SegundoApellido).ToListAsync();
            return listaDocentes;
        }

        public async Task<List<string>> ObtenerDisponibilidadDocente(int idDocente)
        {
            var listarDisponibilidad = await (from a in db.Disponibilidads
                                              join b in db.Docentes
                                              on a.IdDocente equals b.IdDocente
                                              where a.IdDocente == idDocente
                                              select a.Dia).ToListAsync();
            return listarDisponibilidad;
        }

    }
}

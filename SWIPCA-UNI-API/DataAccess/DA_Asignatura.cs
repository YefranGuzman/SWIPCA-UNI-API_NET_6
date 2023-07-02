using System.Data;
using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Asignatura
    {
        DbCargaAcademicaContext cn = new DbCargaAcademicaContext();
        public async Task<List<Asignatura>> ListarAsignaturas()
        {
            var listaAsignaturas = await cn.Asignaturas
                .Select(a => new Asignatura
                {
                    IdAsignatura = a.IdAsignatura,
                    Nombre = a.Nombre,
                    Frecuencia = a.Frecuencia
                })
                .ToListAsync();

            return listaAsignaturas;
        }
        public async Task ActualizarAsignatura(Asignatura ModelAsignatura)
        {
            var asignatura = await cn.Asignaturas.FindAsync(ModelAsignatura.IdAsignatura);
            if (asignatura == null)
            {
                throw new Exception("Asignatura no encontrada");
            }

            asignatura.Nombre = ModelAsignatura.Nombre;
            asignatura.Frecuencia = ModelAsignatura.Frecuencia;

            await cn.SaveChangesAsync();
        }
        public async Task<ActionResult<List<AsignaturasDTO>>> AsignaturasDepartamento(int idUsuario)
        {
            var obtenerdepartamento = await (from a in cn.Usuarios
                                             join b in cn.Docentes
                                             on a.IdUsuario equals b.IdUsuario
                                             join c in cn.Departamentos
                                             on b.IdDepartamento equals c.IdDepartamento
                                             where a.IdUsuario == idUsuario
                                             select c.IdDepartamento).FirstOrDefaultAsync();

            var obtenerfaculta = await (from a in cn.Facultads
                                        join b in cn.Departamentos
                                        on a.IdFacultad equals b.IdFacultad
                                        where b.IdDepartamento == obtenerdepartamento
                                        select a.IdFacultad).FirstOrDefaultAsync();

            var obtenercarrera = await (from a in cn.Carreras
                                        join b in cn.Facultads
                                        on a.IdFacultad equals b.IdFacultad
                                        where b.IdFacultad == obtenerfaculta
                                        select a.IdCarrera).FirstOrDefaultAsync();

            var obtenerAsignaturas = await (from a in cn.Asignaturas
                                            join b in cn.Pensums
                                            on a.IdAsignatura equals b.IdAsignatura
                                            join c in cn.Carreras
                                            on b.IdCarrera equals c.IdCarrera
                                            where c.IdCarrera == obtenercarrera
                                            select new AsignaturasDTO
                                            {
                                                idAsignatura = a.IdAsignatura,
                                                nombreasignatura = a.Nombre,
                                                frecuenciaasignatura = a.Frecuencia
                                            }).ToListAsync();

            return obtenerAsignaturas;
        }
        public class AsignaturasDTO
        {
            public int idAsignatura { get; set; }
            public string nombreasignatura { get; set; }
            public int frecuenciaasignatura { get; set; }
        }
    }
}

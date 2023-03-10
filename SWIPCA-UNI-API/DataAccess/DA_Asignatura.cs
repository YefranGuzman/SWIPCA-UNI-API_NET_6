using System.Data;
using SWIPCA_UNI_API.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

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
    }
}

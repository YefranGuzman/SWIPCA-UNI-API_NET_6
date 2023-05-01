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
        public async Task<List<AsignaturaDTO>> ListarAsignaturasPorDepartamento(int idDepartamento)
        {
            var listAsignaturasDpto = await (from AA in cn.Asignaturas
                                             join PM in cn.Pensums on AA.IdAsignatura equals PM.IdAsignatura
                                             join CR in cn.Carreras on PM.IdCarrera equals CR.IdCarrera
                                             join FT in cn.Facultads on CR.IdFacultad equals FT.IdFacultad
                                             join DP in cn.Departamentos on FT.IdFacultad equals DP.IdFacultad
                                             join DR in cn.Duraccions on PM.IdDuraccion equals DR.IdDuraccion
                                             where DP.IdDepartamento == idDepartamento
                                             select new AsignaturaDTO
                                             {
                                                 Nombre = AA.Nombre,
                                                 Anio = DR.Anio,
                                                 Frecuencia = AA.Frecuencia
                                             }).ToListAsync();

            return listAsignaturasDpto;
        }
        public class AsignaturaDTO
        {
            public string Nombre { get; set; }
            public int Anio { get; set; }
            public int Frecuencia { get; set; }
        }
    }
}

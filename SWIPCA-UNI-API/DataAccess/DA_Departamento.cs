using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Models;
using System.Data;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Departamento
    {
        DbCargaAcademicaContext conexion = new DbCargaAcademicaContext();

        public async Task<List<string>> ListarAsignaturasDeDocentesporHorario(int IdDepartamento, int IdClase)
        {
            DateTime fechaActual = DateTime.Now;
            DayOfWeek diaActual = fechaActual.DayOfWeek;
            TimeSpan horaActual = fechaActual.TimeOfDay;

            var ListAsignaturasDocente = await (
                from DP in conexion.Departamentos
                join FT in conexion.Facultads on DP.IdFacultad equals FT.IdFacultad
                join CR in conexion.Carreras on FT.IdFacultad equals CR.IdFacultad
                join PM in conexion.Pensums on CR.IdCarrera equals PM.IdCarrera
                join AA in conexion.Asignaturas on PM.IdAsignatura equals AA.IdAsignatura
                join HR in conexion.Horarios on CR.IdCarrera equals HR.IdCarrera
                join CL in conexion.Clases on HR.IdClase equals CL.IdClase
                join DC in conexion.Docentes on CL.IdDocente equals DC.IdDocente
                where DP.IdDepartamento == IdDepartamento && CL.IdClase == IdClase && CL.Dia == diaActual.ToString() && CL.HoraInicio <= horaActual && CL.HoraFinal >= horaActual
                select $"{AA.Nombre} {CL.Dia} {CL.HoraInicio} {DC.IdDocente}" // agregar columna de dia
            ).ToListAsync();

            return ListAsignaturasDocente;

        }

        public ActionResult ObtenerDepatamento(int IdDepartamento)
        {
            var empleados = from e in conexion.Departamentos
                            where e.IdDepartamento == IdDepartamento
                            select new { e.Nombre };

            return (ActionResult)empleados;
        }
    }
}

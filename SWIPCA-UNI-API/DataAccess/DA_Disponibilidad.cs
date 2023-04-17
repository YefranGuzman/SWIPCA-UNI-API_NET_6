using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWIPCA_UNI_API.Controllers;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.DataAccess
{
    public class DA_Disponibilidad
    {
        DbCargaAcademicaContext db = new DbCargaAcademicaContext();

        public async Task<string> GuardarDisponibilidadDocente(int IdDocente, string Observacion, int Periodo, string Evidencia, int Estado, int TipoJustificacion)
        {
            if (IdDocente == 0)
            {
                return "El Id del docente no puede venir vacio";
            }

            var save = new Disponibilidad
            {
                IdDocente = IdDocente,
                Observacíon = Observacion,
                Fecha = DateTime.Now,
                Periodicidad = Periodo,
                Evidencia = Evidencia,
                Estado = Estado,
                TipoJustificación = TipoJustificacion
            };

            db.Disponibilidads.Add(save);
            await db.SaveChangesAsync();

            return "Disponibilidad guardada exitosamente";
        }
        public async Task<List<Disponibilidad>> ObtenerDisponibilidadesPorEstado(int idDocente)
        {
            int Estado = 0;
            var disponibilidades = await db.Disponibilidads
                .Include(d => d.IdDocenteNavigation)
                .Where(d => d.IdDocente == idDocente && d.Estado.Equals(Estado))
                .ToListAsync();
            if(disponibilidades.Count == 0)
            {
                return new List<Disponibilidad>() { new Disponibilidad() { Observacíon = "No se encontraron disponibilidades activas." } };
            }

            return disponibilidades;
        }
        public async Task<List<Disponibilidad>> ObtenerDisponibilidadesTodosDocentesPorDepartamento(int idDocente)
        {
            int Estado = 0;

            var DepartamentoJefe = await (from dpto in db.Departamentos
                                          join doc in db.Docentes
                                          on dpto.Jefe equals doc.IdDocente
                                          where doc.IdDocente.Equals(idDocente)
                                          select doc.IdDocente).FirstOrDefaultAsync();

            var CargaAcademica = await (from C_Academica in db.CargaAcademicas
                                        join doc in db.Docentes
                                        on C_Academica.IdDocente equals doc.IdDocente
                                        join Dpto in db.Departamentos
                                        on C_Academica.IdJefe equals Dpto.Jefe
                                        where C_Academica.IdJefe == DepartamentoJefe
                                        select C_Academica.IdDocente).ToListAsync();

            var disponibilidades = await db.Disponibilidads
                .Include(d => d.IdDocenteNavigation)
                .Where(d => CargaAcademica.Contains(d.IdDocente) && d.Estado.Equals(Estado))
                .ToListAsync();

            if (disponibilidades.Count == 0)
            {
                throw new Exception("Todos los docentes tienen disponibilidad de Tiempo");
            }

            return disponibilidades;
        }

    }
}

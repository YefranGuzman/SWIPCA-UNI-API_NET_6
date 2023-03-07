using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Asignatura
{
    public int IdAsignatura { get; set; }

    public string Nombre { get; set; } = null!;

    public int Credito { get; set; }

    public int Frecuencia { get; set; }

    public int IdArea { get; set; }

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual Area IdAreaNavigation { get; set; } = null!;
}

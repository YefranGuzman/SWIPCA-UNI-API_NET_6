using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Duraccion
{
    public int IdDuraccion { get; set; }

    public int Anio { get; set; }

    public int Periodo { get; set; }

    public virtual ICollection<Carrera> Carreras { get; } = new List<Carrera>();
}

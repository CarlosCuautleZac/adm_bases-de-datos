using System;
using System.Collections.Generic;

#nullable disable

namespace ControlEmpleados.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? Sueldo { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; }
    }
}

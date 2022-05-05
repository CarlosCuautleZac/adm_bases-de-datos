using System;
using System.Collections.Generic;

#nullable disable

namespace ControlEmpleados.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? TotalEmpleados { get; set; }
        public decimal SueldoMaximo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}

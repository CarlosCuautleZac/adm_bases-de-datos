using ControlEmpleados.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlEmpleados.ViewModels
{
    public class ReportesViewModel: INotifyPropertyChanged
    {
        NominaContext context = new();

        public ICommand VerEmpleadosCommand { get; set; }

        //Propiedad para el total de empleados
        public int TotalEmpleados { get { return context.Empleados.Count(); } }

        //Propiedad para el total de secciones 
        public int TotalSecciones { get { return context.Categoria.Count(); } }

        //Propiedad para el total de sueldo

        public decimal? MontoTotalSueldosEmpleados { get { return context.Empleados.Sum(x => x.Sueldo); } }

        //Secciones 
        private List<Categorium> categorias;
        public List<Categorium> Categorias
        {
            get
            {
                return categorias;
            }
            set
            {
                categorias = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Categorias"));
            }
        }

        private List<Empleado> empleados;

        public List<Empleado> Empleados
        {
            get { return empleados; }
            set { empleados = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Empleados")); }
        }



        public ReportesViewModel()
        {
            Categorias = context.Categoria.ToList();
            VerEmpleadosCommand = new RelayCommand<Categorium>(EmpleadosByCategoria);
        }
        


        public void EmpleadosByCategoria(Categorium categorium) 
        {

            Empleados = context.Empleados.Include(x => x.IdCategoriaNavigation).Where(x => x.IdCategoria == categorium.Id).ToList();
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}

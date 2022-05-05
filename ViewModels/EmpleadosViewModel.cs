using ControlEmpleados.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlEmpleados.ViewModels
{
    class EmpleadosViewModel : INotifyPropertyChanged
    {
        NominaContext context = new();

        //Comandos

        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        //Propiedad para tener un empleado el cual podemos agregar, editar, eliminar
        private Empleado empleado;

        public Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; Actualizar("Empleado");}
        }

        //Propiedad para manejar los erroes

        private string error;

        public string Erorr
        {
            get { return error; }
            set { error = value; Actualizar("Error"); }
        }

        //Propiedad para manejar lo que hay dentro del usercontrol

        public string Modo { get; set; } = "Ver";

        //Propiedad para tener una coleccion de secciones 
        public List<Categorium> Categorias { get; set; } = new();

        //Propiedad para tener una coleccion de empleados actualizable
        private ObservableCollection<Empleado> empleados;

        public ObservableCollection<Empleado> Empleados
        {
            get { return empleados; }
            set { empleados = value; Actualizar("Empleados"); }
        }



        public EmpleadosViewModel()
        {
            AgregarCommand = new RelayCommand(Agregar);
            Categorias = new(context.Categoria);
            ActualizarListaEmpleados();
        }

        private void Agregar()
        {
            throw new NotImplementedException();
        }

        //Aqui actualizamos empleados
        private void ActualizarListaEmpleados()
        {
            Empleados = new(context.Empleados.Include(x=> x.IdCategoriaNavigation).OrderBy(x=>x.Nombre));
            Actualizar("Empleados");
        }
        public void Actualizar(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

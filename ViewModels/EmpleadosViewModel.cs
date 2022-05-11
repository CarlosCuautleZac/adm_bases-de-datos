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
    public class EmpleadosViewModel : INotifyPropertyChanged
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
            set { empleado = value; Notificar("Empleado");}
        }

        //Propiedad para manejar los erroes

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; Notificar("Error"); }
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
            set { empleados = value; Notificar("Empleados"); }
        }


        //Constructor
        public EmpleadosViewModel()
        {
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
            EditarCommand = new RelayCommand(Editar);
            CancelarCommand = new RelayCommand(Cancelar);
            GuardarCommand = new RelayCommand(Guardar);

            Categorias = new(context.Categoria);
            ActualizarListaEmpleados();
            Notificar("");
        }

        private void Cancelar()
        {
            if (Empleado != null)
                context.Entry(Empleado).Reload();
            Modo = "Ver";
            Error = "";
            Notificar("");
        }

        private void Editar()
        {
            Error = "";
            Modo = "Editar";
            Notificar("");
            Cancelar();
        }

        private void Eliminar()
        {
            Error = "";
            context.Empleados.Remove(context.Empleados.FirstOrDefault(x => x.Id == Empleado.Id));
            Notificar("");
            context.SaveChanges();
        }

        private void Guardar()
        {
            if(Empleado != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Empleado.Nombre))
                    Error += "Escriba el nombre del emplado" + Environment.NewLine;
                if (Empleado.Sueldo <= 0)
                    Error += "El sueldo del empleado debe ser mayor a $0.00" + Environment.NewLine;

                Notificar("Error");

                if(Error == "")
                {
                    if (Empleado.Id == 0)
                        context.Add(Empleado);
                    else
                        context.Update(Empleado);

                    context.SaveChanges();
                    Notificar("");
                    Cancelar();
                }

                
            }
        }

        private void Agregar()
        {
            Empleado = new();
            Error = "";
            Modo = "Agregar";
            Notificar("");
        }

        //Aqui actualizamos empleados
        private void ActualizarListaEmpleados()
        {
            Empleados = new(context.Empleados.Include(x=> x.IdCategoriaNavigation).OrderBy(x=>x.Nombre));
            Notificar("Empleados");
        }
        public void Notificar(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

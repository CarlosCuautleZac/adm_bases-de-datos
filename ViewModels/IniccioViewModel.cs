using ControlEmpleados.Models;
using ControlEmpleados.Views;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ControlEmpleados.ViewModels
{
    public class IniccioViewModel : INotifyPropertyChanged
    {
        public ICommand AdministrarEmpleadosCommand { get; set; }
        public ICommand AdministrarSeccionesCommand { get; set; }
        public ICommand IniciarCommand { get; set; }

        EmpleadosView empleadosview;
        ReportesView reportesview;
        SeccionesView seccionesview;


        NominaContext context = new();

        private UserControl vista;

        public UserControl Vista

        {
            get { return vista; }
            set { vista = value; }
        }

        //Constructor
        public IniccioViewModel()
        {         
            AdministrarEmpleadosCommand = new RelayCommand(AdministrarEmpleados);
            AdministrarSeccionesCommand = new RelayCommand(AdministrarSecciones);
            IniciarCommand = new RelayCommand(Iniciar);           
            Iniciar();
        }

        //CRUD 

        //CREATE

        //READ
        public void GetAllEpleados()
        {
            context.Empleados.Include(x => x.IdCategoriaNavigation);
        }

        public void GetEmpleadosBySection(Categorium categorium)
        {
            context.Empleados.Where(x => x.IdCategoria == categorium.Id);
        }

        //UPDATE

        //DELETE

        //METODOS PARA 

        private void AdministrarEmpleados()
        {
            empleadosview = new() { DataContext = this };
            Vista = empleadosview;
            Actualizar();
        }

        private void AdministrarSecciones()
        {
            seccionesview = new() { DataContext = this };
            Vista = seccionesview;
            Actualizar();
        }

        private void Iniciar()
        {
            reportesview = new() { DataContext = this};
            Vista = reportesview;
            Actualizar();
        }

        public void Actualizar(string nombre = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

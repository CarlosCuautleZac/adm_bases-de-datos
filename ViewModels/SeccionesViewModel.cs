using ControlEmpleados.Models;
using GalaSoft.MvvmLight.Command;
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
    public class SeccionesViewModel : INotifyPropertyChanged
    {
        NominaContext context = new();

        public ICommand VerAgregarCommand { get; set; }
        public ICommand GuardarCommad { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; Actualizar(); }
        }

        private Categorium categorium;

        public Categorium Categoria
        {
            get { return categorium; }
            set { categorium = value; Actualizar(); }
        }

        private ObservableCollection<Categorium> categoria;

        public ObservableCollection<Categorium> Categorias
        {
            get { return categoria; }
            set { categoria = value; Actualizar(); }
        }


        private string modo;

        public string Modo
        {
            get { return modo; }
            set { modo = value; Actualizar("Modo"); }
        }


        //Constructor
        public SeccionesViewModel()
        {
            Modo = "Ver";
            Actualizar("Modo");
            VerAgregarCommand = new RelayCommand(VerAgregar);
            VerEditarCommand = new RelayCommand(VerEditar);
            GuardarCommad = new RelayCommand(Guardar);
            CancelarCommand = new RelayCommand(Cancelar);
            EliminarCommand = new RelayCommand(Eliminar);
            Categorias = new ObservableCollection<Categorium>(context.Categoria.OrderBy(x=>x.Nombre));
        }

        //CRUD

        //CREATE

        private void VerAgregar()
        {
            Error = "";
            Categoria = new();
            Modo = "Agregar";
            Actualizar("Modo");
        }

        private void Guardar()
        {
            if (Categoria != null)
            {
                Error = "";

                if (string.IsNullOrWhiteSpace(Categoria.Nombre))
                    Error += "Escriba el nombre del emplado" + Environment.NewLine;
                if (Categoria.SueldoMaximo < 100)
                    Error += "El sueldo debe ser mayor a $100 como minimo"+Environment.NewLine;

                Actualizar("Error");

                if (Error == "")
                {
                    if (Categoria.Id == 0)
                        context.Add(Categoria);
                    else
                        context.Update(Categoria);

                    context.SaveChanges();
                    Actualizar("");
                    ActualizarListaCategorias();
                    Cancelar();
                }


            }
        }

        //READ 

        private void ActualizarListaCategorias()
        {
            Categorias = new(context.Categoria.OrderBy(x => x.Nombre));
            Actualizar();
        }

        //UPDATE

        private void VerEditar()
        {
            Modo = "Editar";
            Error = "";
            Actualizar("Modo");
        }


        //DELETE

        private void Eliminar()
        {
            Error="";
            if (Categoria.TotalEmpleados < 0)
            {
                context.Remove(Categoria);
                context.SaveChanges();
                ActualizarListaCategorias();
            }
            else
                Error = "No se puede eliminar una categoria que contenga empleados" + Environment.NewLine;
            
        }

        //Comando para cancelar
        
        private void Cancelar()
        {
            if (categoria != null)
                context.Entry(Categoria).Reload();
            Error = "";
            Modo = "Ver";
            Actualizar();

        }

        private void Actualizar(string nombre=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

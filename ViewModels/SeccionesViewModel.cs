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

        //Constructor
        public SeccionesViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);

            Categorias = new ObservableCollection<Categorium>(context.Categoria.OrderBy(x=>x.Nombre));
        }

        //CRUD

        //CREATE

        private void VerAgregar()
        {
            throw new NotImplementedException();
        }

        //READ 

        //UPDATE

        //DELETE



        private void Actualizar(string nombre=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

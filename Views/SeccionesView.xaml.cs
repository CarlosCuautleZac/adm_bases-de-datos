using ControlEmpleados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlEmpleados.Views
{
    /// <summary>
    /// Lógica de interacción para SeccionesView.xaml
    /// </summary>
    public partial class SeccionesView : UserControl
    {
        public SeccionesView()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Eliminar", "Categoria a eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var vm = (SeccionesViewModel)this.DataContext;
                var commandparameter = ((Hyperlink)sender).CommandParameter;
                vm.EliminarCommand.Execute(null);
            }
        }
    }
}

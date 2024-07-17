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

namespace NirvanaEditor.Hub
{
    /// <summary>
    /// Lógica de interacción para VistaCrearProyecto.xaml
    /// </summary>
    public partial class VistaCrearProyecto : UserControl
    {
        public VistaCrearProyecto()
        {
            InitializeComponent();
        }

        private void UIBotonCrearProyecto(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as CrearProyecto;
            var ruta = vm.CrearNuevoProyecto(cajaPlantillas.SelectedItem as PlantillaProyecto);
            bool resultado = false;
            var win = Window.GetWindow(this);
            if (!string.IsNullOrEmpty(ruta))
            {
                resultado = true;
            }
            win.DialogResult = resultado;
            win.Close();
        }
    }
}

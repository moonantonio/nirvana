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
using System.Windows.Shapes;

namespace NirvanaEditor.Hub
{
    /// <summary>
    /// Lógica de interacción para HubProyectos.xaml
    /// </summary>
    public partial class HubProyectos : Window
    {
        public HubProyectos()
        {
            InitializeComponent();
        }

        private void UIBotonMenu(object sender, RoutedEventArgs e)
        {
            if (sender == btnAbrirProyecto)
            {
                if (btnCrearProyecto.IsChecked == true)
                {
                    btnCrearProyecto.IsChecked = false;
                    contenido.Margin = new Thickness(0);
                }
                btnAbrirProyecto.IsChecked = true;
            }
            else 
            {
                if (btnAbrirProyecto.IsChecked == true)
                {
                    btnAbrirProyecto.IsChecked = false;
                    contenido.Margin = new Thickness(-800,0,0,0);
                }
                btnCrearProyecto.IsChecked = true;
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using di.proyecto.clase._2025.MVVM;
using Microsoft.Extensions.DependencyInjection;

namespace di.proyecto.clase._2025.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for UCAdministracion.xaml
    /// </summary>
    public partial class UCAdministracion : UserControl
    {
        private UCArbolEspacios _ucArbolEspacios;
        public UCAdministracion(UCArbolEspacios ucArbolEspacios)
        {
            InitializeComponent();
            _ucArbolEspacios = ucArbolEspacios;
        }

         private void btnEspacios_Click(object sender, RoutedEventArgs e)
         {
            PanelControl.Children.Clear();
            PanelControl.Children.Add(_ucArbolEspacios);
         }
    }
}

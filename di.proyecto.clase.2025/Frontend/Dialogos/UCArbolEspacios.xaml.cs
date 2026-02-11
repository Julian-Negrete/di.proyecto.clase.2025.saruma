using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.MVVM;
using System;
using MahApps.Metro.Controls;
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

namespace di.proyecto.clase._2025.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for UCArbolEspacios.xaml
    /// </summary>
    public partial class UCArbolEspacios : UserControl
    {
        private MVEspacio _mvEspacio;

        public UCArbolEspacios(MVEspacio mVEspacio)
        {
            InitializeComponent();
            _mvEspacio = mVEspacio;
        }

        private async void ucArbolEspacios_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvEspacio.Inicializa();
            DataContext = _mvEspacio;
        }

        private void treeEspacios_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeEspacios.SelectedItem is Espacio) 
            { 
                dgArticulosPorEspacio.ItemsSource = ((Espacio)treeEspacios.SelectedItem).Articulos;
            }
        }

        private async void btnModificarArticulo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                await _mvEspacio.GuardarArticuloAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el modelo de artículo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using di.proyecto.clase._2025.MVVM;
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
    }
}

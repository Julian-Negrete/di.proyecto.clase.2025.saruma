using di.proyecto.clase._2025.Backend.Modelos;
using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for UCArticulos.xaml
    /// </summary>
    public partial class UCArticulos : UserControl
    {
        private DialogoArticulo _dialogoArticulo;
        private DialogoModeloArticulo _dialogoModeloArticulo;
        private readonly IServiceProvider _serviceProvider;
        private UCListarArticulos _ucListarArticulos;
        private UCListarModelos _ucListarModelos;
        public UCArticulos(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnListarArticulos_Click(object sender, RoutedEventArgs e)
        {
            _ucListarArticulos = _serviceProvider.GetRequiredService<UCListarArticulos>();
            PanelControl.Children.Clear();
            PanelControl.Children.Add(_ucListarArticulos);

        }

        private void btnlistarModelos_Click(object sender, RoutedEventArgs e)
        {
            _ucListarModelos = _serviceProvider.GetRequiredService<UCListarModelos>();
            PanelControl.Children.Clear();
            PanelControl.Children.Add(_ucListarModelos);

        }


        private void btnAgregarModelo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo = _serviceProvider.GetRequiredService<DialogoModeloArticulo>();
            _dialogoModeloArticulo.ShowDialog();
            
        }
        private void btnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoArticulo = _serviceProvider.GetRequiredService<DialogoArticulo>();
            Articulo nuevoArticulo = new Articulo();
            _dialogoArticulo.Inicializa(nuevoArticulo);
            _dialogoArticulo.ShowDialog();
            
        }
    }
}

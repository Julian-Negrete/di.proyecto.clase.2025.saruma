using di.proyecto.clase._2025.MVVM;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace di.proyecto.clase._2025.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for UCListarArticulos.xaml
    /// </summary>
    public partial class UCListarArticulos : UserControl
    {
        private MVArticulo _mvArticulo;
        private DialogoArticulo _dialogoArticulo;
        private IServiceProvider _serviceProvider;
        public UCListarArticulos(MVArticulo mvArticulo, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _mvArticulo = mvArticulo;
            _serviceProvider = serviceProvider;
        }

        private async void ListarArticulos_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvArticulo.Inicializa();
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvArticulo.OnErrorEvent));
            DataContext = _mvArticulo;
        }

        private async void editarArticuloLinea_click(object sender, RoutedEventArgs e) { 
            _dialogoArticulo = _serviceProvider.GetRequiredService<DialogoArticulo>();
            await _dialogoArticulo.Inicializa(_mvArticulo.articulo);
            _dialogoArticulo.ShowDialog();
            if (_dialogoArticulo.DialogResult == true) {
                _mvArticulo.listaArticulos.Refresh();
            }
        }

        private void cbModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mvArticulo.Filtrar();
        }
        private void cbEspacios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mvArticulo.Filtrar();
        }

        private void Limpiarfiltros_Click(object sender, RoutedEventArgs e)
        {
            _mvArticulo.LimpiarFiltro();
        }
    }


}

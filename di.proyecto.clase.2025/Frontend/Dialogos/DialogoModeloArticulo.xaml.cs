using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using MahApps.Metro.Controls;
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

namespace di.proyecto.clase._2025.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for DialogoModeloArticulo.xaml
    /// </summary>
    public partial class DialogoModeloArticulo : MetroWindow
    {
        private DiinventarioexamenContext _context;
        private ModeloArticuloRespository _modeloArticuloRepository;
        private TipoArticuloRepository _tipoArticuloRepository;
        public DialogoModeloArticulo()
        {
            InitializeComponent();
        }

        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new DiinventarioexamenContext();
            _modeloArticuloRepository = new ModeloArticuloRespository(_context, null);
            _tipoArticuloRepository = new TipoArticuloRepository(_context, null);

            //Cargamos los tipos de artículo en el ComboBox
            cmbTipoArticulo.ItemsSource = _tipoArticuloRepository.GetAllAsync().Result.ToList();
        }

        private async void btnGuardarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            Modeloarticulo modeloarticulo = new Modeloarticulo();
            RecogeDatos(modeloarticulo);

            try
            {
                await _modeloArticuloRepository.AddAsync(modeloarticulo);
                _context.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el modelo de artículo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancelarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void RecogeDatos(Modeloarticulo modeloarticulo)
        {

        }

    }
}

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
    /// Lógica de interacción para UCListaUsuarios.xaml
    /// </summary>
    public partial class UCListaUsuarios : UserControl
    {
        private MVUsuario _mvUsuario;
        private IServiceProvider _serviceProvider;

        public UCListaUsuarios(MVUsuario mVUsuario, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _mvUsuario = mVUsuario;
            _serviceProvider = serviceProvider;
        }

        private async void ListarUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvUsuario.Inicializa();
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvUsuario.OnErrorEvent));
            DataContext = _mvUsuario;
        }

        private void editarUsuarioContexto_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}

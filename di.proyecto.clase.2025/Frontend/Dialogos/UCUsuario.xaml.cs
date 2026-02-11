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
    /// Lógica de interacción para UCUsuario.xaml
    /// </summary>
    public partial class UCUsuario : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private UCListaUsuarios _ucListaUsuarios;
        public UCUsuario(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider=serviceProvider;
        }

        private void btnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            _ucListaUsuarios = _serviceProvider.GetRequiredService<UCListaUsuarios>();
            PanelControl.Children.Clear();
            PanelControl.Children.Add(_ucListaUsuarios);
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

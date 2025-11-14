using di.proyecto.clase._2025.Frontend.Dialogos;
using MahApps.Metro.Controls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login ventanaLogin = new Login();
            ventanaLogin.Show();
            this.Close();
        }

        private void hamMenuPrincipal_ItemClick(object sender, ItemClickEventArgs args)
        {
            //identificamos el item clicado. SI es un HamburgerMenyUtem, obtenemos su tag
            var hmi = args.ClickedItem as HamburgerMenuItem;
            string etiqueta;

            if (hmi.Tag != null)
            {
                etiqueta = hmi.Tag.ToString();
            } else
            {
                etiqueta = string.Empty;
            }

            switch (etiqueta)
            {
                case "Articulos":
                    hamMenuPrincipal.Content = new UCArticulos();
                    break;
                default:
                    break;
            }

        }
    }
}
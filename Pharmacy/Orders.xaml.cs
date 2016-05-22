using System.Windows;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Btn_selectDataOK2_Copy2_OnClick(object sender, RoutedEventArgs e)
        {
            //Prod
            var newWind = new Products.Products();
            newWind.ShowDialog();
        }

        private void Btn_selectDataOK2_Copy1_OnClick(object sender, RoutedEventArgs e)
        {
            //Clients
            var newWind = new Clients();
            newWind.ShowDialog();
        }

        private void Btn_selectDataOK2_Copy_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

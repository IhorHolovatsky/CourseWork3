using System.Windows;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void Btn_selectDataOK2_Copy1_OnClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}

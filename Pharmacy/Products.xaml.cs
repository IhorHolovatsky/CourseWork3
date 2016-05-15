using System.Windows;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Btn_selectDataOK2_Copy_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.Windows;

namespace Pharmacy.Products
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

        private void NewPruduct_OnClick(object sender, RoutedEventArgs e)
        {
           var newProduct = new NewProduct();
            newProduct.ShowDialog();
        }
    }
}

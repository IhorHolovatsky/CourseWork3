using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void MainWindow_OnLoaded(object sender, EventArgs e)
        {
            MenuClients.IsEnabled = false;
            MenuOrders.IsEnabled = false;
            MenuProduct.IsEnabled = false;
        }

        public void Btn_OnClick(object sender, EventArgs e)
        {
            MenuClients.IsEnabled = true;
            MenuOrders.IsEnabled  = true;
            MenuProduct.IsEnabled = true;
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new About();
            newWindow.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuProduct_OnClick(object sender, RoutedEventArgs e)
        {
            var newProduc = new Products();
            newProduc.Show();
        }

        private void MenuClients_OnClick(object sender, RoutedEventArgs e)
        {
            var newProduc = new Clients();
            newProduc.Show();
        }

        private void MenuOrders_OnClick(object sender, RoutedEventArgs e)
        {
            var newProduc = new Orders();
            newProduc.Show();
        }

        private void ExitClick(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in this.OwnedWindows)
            {
                MessageBox.Show(window.Title);
                window.Close();
            }
            Environment.Exit(0);
        }
    }
}

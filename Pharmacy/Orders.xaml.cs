using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;

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

            comboBoxClients.ItemsSource = PatientManager.GetAll();
            ordersGrid.ItemsSource = GetGridData();
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

        private void FindOrder_OnClick(object sender, RoutedEventArgs e)
        {
            ordersGrid.ItemsSource = GetGridData();
        }

        private void FindAll_OnClick(object sender, RoutedEventArgs e)
        {
            comboBoxClients.SelectedIndex = -1;
            clientPhone.Text = null;
            trackOrderId.Text = null;

            ordersGrid.ItemsSource = GetGridData();
        }

        private List<Order> GetGridData()
        {
            var orders = OrderManager.GetAll();

            orders = orders.Where(o => string.IsNullOrEmpty(clientPhone.Text) || o.Recipe.Patient.PhoneNumber.Contains(clientPhone.Text))
                           .Where(o => comboBoxClients.SelectedItem == null || o.Recipe.Patient.Id == ((Patient)comboBoxClients.SelectedItem).Id)
                           .Where(o => string.IsNullOrEmpty(trackOrderId.Text) || o.Id == Guid.Parse(trackOrderId.Text))
                           .ToList();

            return orders;
        }
    }
}

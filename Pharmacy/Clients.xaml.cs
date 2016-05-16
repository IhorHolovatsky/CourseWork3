using System.Windows;
using Pharmacy.BusinessLogic.Data;

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

            var patients = PatientManager.GetAll();

            PatientsGrid.ItemsSource = patients;
        }

        private void Btn_selectDataOK2_Copy1_OnClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;

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

            ShowAllClientClick(this, new RoutedEventArgs());
        }

        private void Btn_selectDataOK2_Copy1_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindClientClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_ClientId.Text))
            {
                Guid patientId;
                if (!Guid.TryParse(tb_ClientId.Text, out patientId))
                {
                    MessageBox.Show("Invalid Patient ID");
                    return;
                }

                var patient = PatientManager.GetPatientById(patientId);
                PatientsGrid.ItemsSource = new List<Patient> { patient };
                
                return;
            }


            var patients = new List<Patient>();

            if (!string.IsNullOrEmpty(tb_ClientPhone.Text))
            {
                patients = PatientManager.GetPatientByPhone(tb_ClientPhone.Text);
                patients = patients.Where(p => string.IsNullOrEmpty(tb_ClientLastName.Text) || p.LastName == tb_ClientLastName.Text)
                                   .ToList();
            }
            else if (!string.IsNullOrEmpty(tb_ClientLastName.Text))
            {
                patients = PatientManager.GetPatientByName(tb_ClientLastName.Text);
                patients = patients.Where(p => string.IsNullOrEmpty(tb_ClientPhone.Text) || p.PhoneNumber == tb_ClientPhone.Text)
                                   .ToList();
            }


            PatientsGrid.ItemsSource = patients;
        }

        private void ShowAllClientClick(object sender, RoutedEventArgs e)
        {
            tb_ClientId.Text = null;
            tb_ClientLastName.Text = null;
            tb_ClientPhone.Text = null;

            var patients = PatientManager.GetAll();

            PatientsGrid.ItemsSource = patients;
        }
        private void Btn_selectClient_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Grid

        private void PatientsGrid_OnSelected(object sender, RoutedEventArgs e)
        {
            btn_selectClient.IsEnabled = PatientsGrid.SelectedCells.Count > 0;
        }

        #endregion

        private void AddNewClientClick(object sender, RoutedEventArgs e)
        {
            var patient = new Patient()
            {
                FirstName = tb_PatientFirstName.Text,
                LastName = tb_PatientLastName.Text,
                SecondaryName = tb_PatientSecondaryName.Text,
                DateOfBirth = tb_PatientDateOfBirth.DisplayDate,
                PhoneNumber = tb_PatientPhoneNumber.Text,
                Address = tb_PatientAddress.Text
            };

            var insertedPatientId = PatientManager.Insert(patient);
            var insertedPatient = PatientManager.GetPatientById(insertedPatientId);

           // PatientsGrid.Items.Add(insertedPatient);
        }
    }
}

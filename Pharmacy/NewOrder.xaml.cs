using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.Comparers;
using Pharmacy.DatabaseAccess.Enums;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        private Recipe Recipe { get; set; }

        public NewOrder()
        {
            InitializeComponent();
            Recipe = new Recipe()
            {
                Medicines = new List<Medicine>() 
            };
        }

        private void SelectClient_OnClick(object sender, RoutedEventArgs e)
        {
            var clientForm = new Clients();
            clientForm.ShowDialog();

            if (clientForm.SelectedPatient == null)
            {
                MessageBox.Show("Patient was not selected");
                return;
            }
            Recipe.Patient = clientForm.SelectedPatient;

            InitFormData(Recipe);
        }

        private void AddMedicine_OnClick(object sender, RoutedEventArgs e)
        {
            var productsForm = new Products.Products();
            productsForm.ShowDialog();

            if (productsForm.SelectedMedicines == null || !productsForm.SelectedMedicines.Any())
            {
                MessageBox.Show("Medicines were not selected");
                return;
            }

            Recipe.Medicines.AddRange(productsForm.SelectedMedicines);
            Recipe.Medicines = Recipe.Medicines.Distinct(new EntityComparer())
                                               .Cast<Medicine>()
                                               .ToList();

            medicinesGrid.ItemsSource = Recipe.Medicines;
        }

        private void DeleteMedicine_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedMedicines = medicinesGrid.SelectedItems.Cast<Medicine>();
            Recipe.Medicines = Recipe.Medicines.Except(selectedMedicines).ToList();
            medicinesGrid.ItemsSource = Recipe.Medicines;
        }

        private void Dismiss_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitFormData(Recipe recipe)
        {
            diagnoz.Text = recipe.Diagnoz;

            if (recipe.Patient != null)
            {
                patientFirstName.Text = recipe.Patient.FirstName;
                patientSecondName.Text = recipe.Patient.LastName;
                patientFathersName.Text = recipe.Patient.SecondaryName;
                patientDob.SelectedDate = recipe.Patient.DateOfBirth;
                patientAddress.Text = recipe.Patient.Address;
                patientPhone.Text = recipe.Patient.PhoneNumber;
            }

            if (recipe.Doctor != null)
            {
                doctorFirstName.Text = recipe.Doctor.FirstName;
                doctorLastName.Text = recipe.Doctor.LastName;
                doctorFathersName.Text = recipe.Doctor.SecondaryName;
            }

            if (recipe.Medicines != null && recipe.Medicines.Count > 0)
                medicinesGrid.ItemsSource = recipe.Medicines;
        }

        private void CreateOrder_OnClick(object sender, RoutedEventArgs e)
        {
            if (Recipe.Medicines == null || Recipe.Patient == null)
                return;

            var doctor = new Doctor()
            {
                LastName = doctorLastName.Text,
                FirstName = doctorFirstName.Text,
                SecondaryName = doctorFathersName.Text
            };

            try
            {
                var doctorId = DoctorManager.Insert(doctor);
                Recipe.Doctor = new Doctor(doctorId);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Doctor information is empty");
                return;
            }

            Guid recipeId;

            try
            {
                Recipe.Diagnoz = diagnoz.Text;
                recipeId = RecipeManager.Insert(Recipe);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Recipe information is empty");
                return;
            }

            var order = new Order
            {
                Recipe = new Recipe(recipeId),
                TotalPrice = Recipe.Medicines.Sum(m => m.Price),
                PhoneNumber = Recipe.Patient.PhoneNumber,
                OrderDate = DateTime.Now,
                AvailabilityOfComponents = new Random().Next(100),
                MakeState = OrderState.Open,
                ReadyTime = DateTime.Now.AddDays(new Random().Next(1,10))
            };

            try
            {
                OrderManager.Insert(order);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("order information is empty");
                return;
            }
            this.Close();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;

namespace Pharmacy.Products
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        private List<Ingredient> _ingredients;
        private List<Ingredient> _blockedIngredients = new List<Ingredient>();
        private List<Ingredient> _neededIngredients = new List<Ingredient>();

        public Products()
        {
            InitializeComponent();

            //Init click on products grid
            var rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(ProductsGrid_OnRowClick)));
            productsGrid.RowStyle = rowStyle;

            filterMedicineType.ItemsSource = MedicineTypeManager.GetAll();
            _ingredients = IngredientManager.GetAll();

            IngredientsGrid.ItemsSource = GetIngredientGridData();
            productsGrid.ItemsSource = GetGridData();
        }

        private void Btn_selectDataOK2_Copy_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewPruduct_OnClick(object sender, RoutedEventArgs e)
        {
            var newProduct = new NewProduct();
            newProduct.ShowDialog();

            productsGrid.ItemsSource = GetGridData();
        }


        private void ProductsGrid_OnRowClick(object sender, RoutedEventArgs e)
        {
            var dataRow = (DataGridRow)sender;

            if (!(dataRow.Item is Medicine))
                return;

            var medicine = (Medicine)dataRow.Item;

            var newProduct = new NewProduct();
            newProduct.Init(medicine);
            newProduct.ShowDialog();

            productsGrid.ItemsSource = GetGridData();
        }


        private List<Medicine> GetGridData()
        {
            var medicines = MedicineManager.GetAllMedicines();
            int priceFrom;
            int priceTo;

                medicines = medicines.Where(med => !int.TryParse(filterMedicinePriceFrom.Text, out priceFrom) || med.Price >= priceFrom)
                                     .Where(med => !int.TryParse(filterMedicinePriceFrom.Text, out priceTo) || med.Price <= priceTo)
                                     .Where(med => filterMedicineType.SelectedItem == null || med.Type.Id == ((MedicineType)filterMedicineType.SelectedItem).Id)
                                     .Where(med => string.IsNullOrEmpty(filterMedicineName.Text) || med.Name.ToLower().Contains(filterMedicineName.Text.ToLower()))
                                     .Where(med => _neededIngredients.Count <= 0 || med.Ingredients.Any(ingr=> _neededIngredients.Any(needIngr=> ingr.Id == needIngr.Id)))
                                     .Where(med => _blockedIngredients.Count <= 0 || med.Ingredients.All(ingr => _blockedIngredients.All(needIngr => ingr.Id != needIngr.Id)))
                                     .ToList();
         

            return medicines;
        }

        private void ProductsGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedCells.Count <= 0)
                return;

            DetailsGrid.Visibility = Visibility.Visible;

            var dataRow = dataGrid.SelectedCells.First();

            if (!(dataRow.Item is Medicine)) return;

            var medicine = (Medicine)dataRow.Item;

            medicineImage.Source = medicine.BitmapImage;
            medicineName.Content = medicine.Name;
            medicineType.Content = medicine.Type.TypeOf;

            medicineDescription.Document.Blocks.Clear();
            medicineDescription.Document.Blocks.Add(new Paragraph(new Run(medicine.Description)));
            medicineIngredients.ItemsSource = medicine.Ingredients;

            ButtonDeleteMedicine.IsEnabled = true;
        }

        private void Filter_OnClick(object sender, RoutedEventArgs e)
        {
            productsGrid.ItemsSource = GetGridData();
        }

         private void FilterClear_OnClick(object sender, RoutedEventArgs e)
         {
             filterMedicineType.SelectedIndex = -1;
             filterMedicineName.Text = null;
             filterMedicinePriceFrom.Text = null;
             filterMedicinePriceTo.Text = null;
             BlockedIngredientsGrid.ItemsSource = null;
             NeededIngredientsGrid.ItemsSource = null;
             _neededIngredients.Clear();
             _blockedIngredients.Clear();
             

             productsGrid.ItemsSource = GetGridData();
             IngredientsGrid.ItemsSource = GetIngredientGridData();
         }

         private void ExtentedSearch_OnClick(object sender, RoutedEventArgs e)
         {
             if (ExtentedSearchGroupBox.Visibility == Visibility.Hidden)
             {
                 DetailsGroupBox.Visibility = Visibility.Hidden;
                 ExtentedSearchGroupBox.Visibility = Visibility.Visible;
             }
             else
             {
                 ExtentedSearchGroupBox.Visibility = Visibility.Hidden;
                 DetailsGroupBox.Visibility = Visibility.Visible;
             }
         }

        private void Need_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedIngredients = IngredientsGrid.SelectedItems;

            _neededIngredients.AddRange(selectedIngredients.Cast<Ingredient>().ToList());
            NeededIngredientsGrid.ItemsSource = null;
            NeededIngredientsGrid.ItemsSource = _neededIngredients;
           

            IngredientsGrid.ItemsSource = GetIngredientGridData();
        }
        private void Block_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedIngredients = IngredientsGrid.SelectedItems;

            _blockedIngredients.AddRange(selectedIngredients.Cast<Ingredient>().ToList());
            BlockedIngredientsGrid.ItemsSource = null;
            BlockedIngredientsGrid.ItemsSource = _blockedIngredients;

            IngredientsGrid.ItemsSource = GetIngredientGridData();
        }

        private List<Ingredient> GetIngredientGridData()
        {
            return _ingredients.Where(ingr => !_blockedIngredients.Contains(ingr))
                               .Where(ingr => !_neededIngredients.Contains(ingr))
                               .ToList();
        }

        private void ButtonDeleteMedicine_OnClick(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedCells.Count <= 0)
                return;

            foreach (var medicineRow in productsGrid.SelectedCells)
            {
                if (!(medicineRow.Item is Medicine)) return;

                var medicine = (Medicine)medicineRow.Item;

                MedicineManager.Delete(medicine);
            }

            DetailsGrid.Visibility = Visibility.Hidden;
            medicineImage.Source = null;
            medicineName.Content = null;
            medicineType.Content = null;

            medicineDescription.Document.Blocks.Clear();
            medicineIngredients.ItemsSource = null;

            productsGrid.ItemsSource = GetGridData();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Microsoft.Win32;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;
using Image = System.Drawing.Image;

namespace Pharmacy.Products
{
    /// <summary>
    /// Interaction logic for NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Window
    {
        private bool _isEditMode { get; set; }
        private Medicine _medicine { get; set; }
        private List<Ingredient> _ingredients;
        private List<MedicineType> _medicineTypes;
        private List<Ingredient> _selectedIngredients = new List<Ingredient>();
      
        public NewProduct()
        {
            InitializeComponent();

            //Init double click on ingredients grid
            var rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(IngredientsGrid_OnRowClick)));
            IngredientsGrid.RowStyle = rowStyle;

            _medicineTypes = MedicineTypeManager.GetAll();
            medicineType.ItemsSource = _medicineTypes;

            //init grid and combobox data
            _ingredients = IngredientManager.GetAll();

            IngredientsGrid.ItemsSource = GetGridData();

        }

        public void Init()
        {
            var medicine = (Medicine) Utils.Utils.Deserialize(typeof (Medicine));
            this.Init(medicine);
        }

        public void Init(Medicine medicine)
        {
            _isEditMode = true;
            _medicine = medicine;

            productName.Text = medicine.Name;
            productDescription.Text = medicine.Description;
            medicineType.SelectedItem = _medicineTypes.First(mtype => mtype.Id == medicine.Type.Id);
         
            productPrice.Text = medicine.Price.ToString();
            _selectedIngredients = medicine.Ingredients;
            ingredientsTextBlock.Text = string.Join(", ", _selectedIngredients);
            productImage.Source = medicine.BitmapImage;
            productImageLabel.Visibility = Visibility.Hidden;
            
            IngredientsGrid.ItemsSource = GetGridData();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            // Set filter for file extension and default file extension

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            productImageLabel.Visibility = result ?? false ? Visibility.Hidden : Visibility.Visible;

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                productImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
           _selectedIngredients.Clear();
            ingredientsTextBlock.Text = null;

            IngredientsGrid.ItemsSource = GetGridData();
        }
        
        private void IngredientFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            IngredientsGrid.ItemsSource = GetGridData();
        }

        private void IngredientsGrid_OnRowClick(object sender, RoutedEventArgs e)
        {
            var dataRow = (DataGridRow)sender;

            if (!(dataRow.Item is Ingredient))
                return;

            var ingredient = (Ingredient)dataRow.Item;

            if (_selectedIngredients.Contains(ingredient))
                return;

            _selectedIngredients.Add(ingredient);

            ingredientsTextBlock.Text = string.Join(", ", _selectedIngredients);
            IngredientsGrid.ItemsSource = GetGridData();
        }

        private IEnumerable<Ingredient> GetGridData()
        {
            var gridData = _ingredients.Where(ingr => _selectedIngredients.All(selectedIngr => selectedIngr.Id != ingr.Id))
                                       .ToList(); 

            gridData = gridData.Where(ingr => ingr.Name.StartsWith(ingredientFilter.Text, true, CultureInfo.DefaultThreadCurrentCulture))
                               .ToList();

            return gridData;
        }

        private void SaveMedicineClick(object sender, RoutedEventArgs e)
        {
            var medicine = new Medicine(_medicine == null ? Guid.Empty : _medicine.Id)
            {
                Name = productName.Text,
                Description = productDescription.Text,
                Image = Utils.Utils.BitmapImageToBitmap(productImage.Source as BitmapImage),
                Type = medicineType.SelectionBoxItem as MedicineType,
                Price = decimal.Parse(productPrice.Text),
                Ingredients = _selectedIngredients
            };
            
            if (_isEditMode)
            {
                MedicineManager.Update(medicine);
                this.Close();
                return;
            }
            

            MedicineManager.Insert(medicine);

            productName.Text = null;
            productDescription.Text = null;
            medicineType.SelectedIndex = -1;
            productPrice.Text = null;
            ingredientsTextBlock.Text = null;
            productImage.Source = null;
            ingredientFilter.Text = null;
            _selectedIngredients.Clear();
        }

        private void Init_OnClick(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
              var medicine = new Medicine(_medicine == null ? Guid.Empty : _medicine.Id)
            {
                Name = productName.Text,
                Description = productDescription.Text,
                Image = Utils.Utils.BitmapImageToBitmap(productImage.Source as BitmapImage),
                Type = medicineType.SelectionBoxItem as MedicineType,
                Price = decimal.Parse(productPrice.Text),
                Ingredients = _selectedIngredients
            };

            Utils.Utils.Serialize(medicine);
        }

    }
}

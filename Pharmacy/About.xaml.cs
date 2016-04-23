using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var hl = sender as Hyperlink;
            string address = string.Concat("mailto:", hl.NavigateUri.ToString(),
  "?subject=This is the subject");
            try { System.Diagnostics.Process.Start(address); }
            catch { MessageBox.Show("That e-mail address is invalid.", "E-mail error"); }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

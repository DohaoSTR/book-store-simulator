using System.Windows;
using System.Windows.Controls;

namespace BookStore.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuItemControl.xaml
    /// </summary>
    public partial class MenuItemControl : UserControl
    {
        private ItemMenu _itemMenu;

        public MenuItemControl(ItemMenu itemMenu)
        {
            InitializeComponent();

            _itemMenu = itemMenu;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            DataContext = itemMenu;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            foreach (SubItem item in _itemMenu.SubItems)
            {
                if (item.Name == button.Content.ToString())
                {
                    item.Сlick(sender, e, item);
                }
            }
        }
    }
}

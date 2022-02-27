using System.Windows;
using System.Windows.Controls;

namespace BookStore.View
{
    public class SubItem
    {
        public string Name { get; private set; }

        public UserControl Screen { get; private set; }

        public UserControl EntityControl { get; private set; }

        public delegate void ClickDelegate(object sender, RoutedEventArgs e, SubItem item);

        public ClickDelegate Сlick;

        public SubItem(string name, EntityControl entityControl, ClickDelegate clickDelegate, UserControl screen = null)
        {
            Name = name;
            EntityControl = entityControl;
            Сlick = clickDelegate;
            Screen = screen;
        }
    }
}

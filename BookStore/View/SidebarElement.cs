using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BookStore.View
{
    public enum TypeTextBox
    {
        TextBox,
        IntBox
    }

    public class SidebarElement
    {
        public TextBlock Header { get; private set; } = new TextBlock();

        public Control InputControl { get; private set; }

        public TypeTextBox TypeTextBox { get; private set; }

        public SidebarElement(string header, Control inputControl, TypeTextBox typeTextBox = TypeTextBox.TextBox)
        {
            Header.Text = header;
            InputControl = inputControl;
            TypeTextBox = typeTextBox;

        }

        public object GetControlData()
        {
            object value;
            if (InputControl.GetType() == typeof(Slider))
            {
                value = Convert.ToInt32(InputControl.GetValue(RangeBase.ValueProperty));
            }
            else if (InputControl.GetType() == typeof(DatePicker))
            {
                value = InputControl.GetValue(DatePicker.SelectedDateProperty);
            }
            else if (InputControl.GetType() == typeof(TextBox))
            {
                if (TypeTextBox == TypeTextBox.IntBox)
                {
                    value = Convert.ToInt32(InputControl.GetValue(TextBox.TextProperty));
                }
                else
                {
                    value = InputControl.GetValue(TextBox.TextProperty);
                }
            }
            else
            {
                throw new ArgumentException("Передан неккоректный контрол!");
            }

            return value;
        }
    }
}

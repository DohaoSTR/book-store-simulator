using BookStore.Model.DataBase;
using System;
using System.Windows;

namespace BookStore.View
{
    /// <summary>
    /// Логика взаимодействия для ServerConnectWindow.xaml
    /// </summary>
    public partial class ServerConnectWindow : Window
    {
        private DataModel _dataModel;

        public ServerConnectWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConnectionSettings connectionSettings = new ConnectionSettings(
                HostTextBox.Text, PortTextBox.Text, UserTextBox.Text,
                PasswordTextBox.Password, DefaulSchemaTextBox.Text, CharSetTextBox.Text);

                _dataModel = new DataModel(connectionSettings);
                if (_dataModel.Connect())
                {
                    MessageBox.Show("Соединение успешно!");

                    MainWindow mainWindow = new MainWindow(_dataModel);
                    mainWindow.Show();

                    Close();
                }
                else
                {
                    throw new ArgumentException("Данные введены неверно!");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

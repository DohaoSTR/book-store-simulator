using BookStore.Model.DataBase;
using BookStore.Model.DataBase.Entities;
using BookStore.Model.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.View
{
    /// <summary>
    /// Interaction logic for ExpertSystemControl.xaml
    /// </summary>
    public partial class ExpertSystemControl : UserControl
    {
        private DataModel _dataModel;

        private ObservableCollection<PrintedMatter> _observableEntities;

        public ExpertSystemControl(IEnumerable<DataGridColumn> columns, DataModel dataModel)
        {
            InitializeComponent();

            _dataModel = dataModel;

            foreach (DataGridColumn column in columns)
            {
                EntityDataGrid.Columns.Add(column);
            }
        }

        private void OutputRecommend_Click(object sender, RoutedEventArgs e)
        {
            string typeOfEdition = (TypeOfEditionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            TypeOfEdition type = TypeOfEdition.Electronic;


            if (typeOfEdition == "Электронное издание")
            {
                type = TypeOfEdition.Electronic;
            }
            else if (typeOfEdition == "Бумажное издание")
            {
                type = TypeOfEdition.Paper;
            }

            UserParameters userParameters = new UserParameters(type, Convert.ToInt32(AgeLimitTextBox.Text),
                Convert.ToInt32(MinPriceTextBox.Text), Convert.ToInt32(MaxPriceTextBox.Text));

            List<PrintedMatter> entities = _dataModel.GetRecommendedEntities(userParameters);

            _observableEntities = new ObservableCollection<PrintedMatter>(entities);

            EntityDataGrid.ItemsSource = _observableEntities;
        }
    }
}

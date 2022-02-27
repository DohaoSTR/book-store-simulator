using BookStore.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.View
{
    /// <summary>
    /// Логика взаимодействия для EntityControl.xaml
    /// </summary>
    public partial class EntityControl : UserControl
    {
        private DataModel _dataModel;

        private readonly ObservableCollection<DataBaseEntity> _observableEntities;

        private List<DataBaseEntity> _entities;

        private DataBaseEntity _entity;

        public EntityControl(IEnumerable<DataGridColumn> columns, IEnumerable<SidebarElement> sidebarElements, DataBaseEntity entity, DataModel dataModel)
        {
            InitializeComponent();

            _entity = entity;

            foreach (DataGridColumn column in columns)
            {
                EntityDataGrid.Columns.Add(column);
            }

            _dataModel = dataModel;

            List<DataBaseEntity> entities = _dataModel.GetDataBaseEntities(entity);
            _observableEntities = new ObservableCollection<DataBaseEntity>(entities);
            _entities = new List<DataBaseEntity>(entities);


            EntityDataGrid.ItemsSource = _observableEntities;


            foreach (SidebarElement element in sidebarElements)
            {
                SidebarPanel.Children.Add(element.Header);
                SidebarPanel.Children.Add(element.InputControl);
                _sidebarElements.Add(element);
            }
        }

        private List<SidebarElement> _sidebarElements = new List<SidebarElement>();

        private DataBaseEntity GetEntity()
        {
            object[] valueElements = new object[_sidebarElements.Count];
            Type[] typeElements = new Type[_sidebarElements.Count];

            int i = 0;

            foreach (SidebarElement element in _sidebarElements)
            {
                object value = element.GetControlData();
                valueElements[i] = value;
                typeElements[i] = value.GetType();
                i++;
            }

            Type type = _entity.GetType();

            ConstructorInfo constructor = type.GetConstructor(typeElements);
            object entity = constructor.Invoke(valueElements);

            return (DataBaseEntity)entity;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBaseEntity entity = GetEntity();
                entity.Id = _dataModel.AddEntity(entity);
                _entities.Add(entity);
                _observableEntities.Add(entity);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (EntityDataGrid.SelectedItem is DataBaseEntity entity)
            {
                _dataModel.RemoveEntity(entity);

                _observableEntities.Remove(EntityDataGrid.SelectedItem as DataBaseEntity);
                _entities.Remove(EntityDataGrid.SelectedItem as DataBaseEntity);
            }
            else
            {
                MessageBox.Show("Строка не выбрана!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (EntityDataGrid.SelectedItem is DataBaseEntity selectedEntity)
            {
                int id = _entities.FindIndex(x => x.Id == selectedEntity.Id);

                int index = _observableEntities.IndexOf(EntityDataGrid.SelectedItem as DataBaseEntity);

                DataBaseEntity updatedEntity = GetEntity();
                updatedEntity.Id = selectedEntity.Id;

                _observableEntities[index] = updatedEntity;

                _entities[id] = _observableEntities[index];

                _dataModel.UpdateEntity(_observableEntities[index]);
            }
            else
            {
                MessageBox.Show("Строка не выбрана!");
            }
        }
    }
}

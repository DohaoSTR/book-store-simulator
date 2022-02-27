using BookStore.Model.DataBase;
using BookStore.Model.DataBase.Entities;
using BookStore.View;
using BookStore.View.Menu;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using DataGridTextColumn = System.Windows.Controls.DataGridTextColumn;

namespace BookStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel _dataModel;
        public MainWindow(DataModel dataModel)
        {
            InitializeComponent();

            _dataModel = dataModel;

            List<SubItem> menuProducts = new List<SubItem>
            {
                new SubItem("Товары", GetPrintedMatter(dataModel), OnClickMenuItem),
                new SubItem("Издатели товаров", GetPublisher(dataModel), OnClickMenuItem),
                new SubItem("Авторы товаров", GetAuthor(dataModel), OnClickMenuItem),
                new SubItem("Жанры товаров", GetGenre(dataModel), OnClickMenuItem),
                new SubItem("ISBN коды товаров", GetISBN(dataModel), OnClickMenuItem),
                new SubItem("Связь товара и издателей", GetPrintedMatterPublisher(dataModel), OnClickMenuItem),
                new SubItem("Связь товара и авторов", GetPrintedMatterAuthor(dataModel), OnClickMenuItem),
                new SubItem("Связь товара и жанров", GetPrintedMatterGenre(dataModel), OnClickMenuItem),
                new SubItem("Связь товара и ISBN кодов", GetPrintedMatterISBN(dataModel), OnClickMenuItem)
            };
            ItemMenu productsItem = new ItemMenu("Информация о товарах", menuProducts, PackIconKind.FormatSection);

            List<SubItem> menuStatusProducts = new List<SubItem>
            {
                new SubItem("Прибывшие товары", GetArrivalPrintedMatter(dataModel), OnClickMenuItem),
                new SubItem("Прайс-лист", GetPriceList(dataModel), OnClickMenuItem),
                new SubItem("Проданные товары", GetSoldPrintedMatter(dataModel), OnClickMenuItem)
            };
            ItemMenu productsStatusItem = new ItemMenu("Статусы товара", menuStatusProducts, PackIconKind.ListStatus);

            List<SubItem> menuDocuments = new List<SubItem>
            {
                new SubItem("Сертификаты о соответствии", GetConformityCertificate(dataModel), OnClickMenuItem),
                new SubItem("Декларации о соответствии", GetConformityDeclaration(dataModel), OnClickMenuItem),
                new SubItem("Товарно-транспортные накладные", GetGoodTransportWaybill(dataModel), OnClickMenuItem),
                new SubItem("Санитарно-эпидемиологические сертификаты", GetSanitaryEpidemiologicalCertificate(dataModel), OnClickMenuItem),
                new SubItem("Свидетельства о госрегистрации", GetStateRegistrationCertificate(dataModel), OnClickMenuItem)
            };
            ItemMenu documentsItem = new ItemMenu("Документы", menuDocuments, PackIconKind.FileDocument);

            List<SubItem> menuCards = new List<SubItem>
            {
                new SubItem("Подарочные карты", GetGiftCard(dataModel), OnClickMenuItem),
                new SubItem("Скидочные карты", GetDiscountCard(dataModel), OnClickMenuItem),
                new SubItem("Скидки", GetDiscount(dataModel), OnClickMenuItem)
            };
            ItemMenu cardsAndDiscountsItem = new ItemMenu("Карты и скидки магазина", menuCards, PackIconKind.CreditCard);

            List<SubItem> menuTypeProducts = new List<SubItem>
            {
                new SubItem("Художественная литература", GetStandardBook(dataModel), OnClickMenuItem),
                new SubItem("Пособия и учебные материалы", GetSchoolBook(dataModel), OnClickMenuItem),
                new SubItem("Брошюры", GetBrochure(dataModel), OnClickMenuItem),
                new SubItem("Журналы", GetJournal(dataModel), OnClickMenuItem)
            };
            ItemMenu typesProductItem = new ItemMenu("Виды товаров", menuTypeProducts, PackIconKind.LibraryBookmark);

            List<SubItem> menuEditions = new List<SubItem>
            {
                new SubItem("Печатные издания", GetPaperEdition(dataModel), OnClickMenuItem),
                new SubItem("Электронные издания", GetElectronicEdition(dataModel), OnClickMenuItem)
            };
            ItemMenu typesEditionItem = new ItemMenu("Виды изданий", menuEditions, PackIconKind.BookSettings);

            Menu.Children.Add(new MenuItemControl(productsItem));
            Menu.Children.Add(new MenuItemControl(productsStatusItem));
            Menu.Children.Add(new MenuItemControl(documentsItem));
            Menu.Children.Add(new MenuItemControl(cardsAndDiscountsItem));
            Menu.Children.Add(new MenuItemControl(typesProductItem));
            Menu.Children.Add(new MenuItemControl(typesEditionItem));
        }

        private void ExpertSystem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Заголовок", "Title"),
                GetDataGridColumn("Аннотация", "Annotation"),
                GetDataGridColumn("Кол-во страниц", "NumberOfPages"),
                GetDataGridColumn("Возрастное ограничение", "AgeLimit"),
                GetDataGridColumn("Дата издания", "ImprintDate")
            };

            EntityControlPanel.Children.Clear();
            EntityControlPanel.Children.Add(new ExpertSystemControl(dataGridColumns, _dataModel));

        }

        private void OnClickMenuItem(object sender, RoutedEventArgs e, SubItem item)
        {
            Button button = (Button)sender;

            if (item.Name == button.Content.ToString())
            {
                EntityControlPanel.Children.Clear();
                EntityControlPanel.Children.Add(item.EntityControl);
            }
            else
            {
                throw new Exception();
            }
        }

        private DataGridColumn GetDataGridColumn(string header, string pathBinding)
        {
            DataGridTextColumn column = new DataGridTextColumn
            {
                Header = header
            };
            Binding columnBinding = new Binding
            {
                Path = new PropertyPath(pathBinding)
            };
            column.Binding = columnBinding;

            return column;
        }

        private SidebarElement GetSidebarElement(string header, Control control, string bindingPath, TypeTextBox typeTextBox = TypeTextBox.TextBox)
        {
            Binding binding = new Binding
            {
                ElementName = "EntityDataGrid",
                Path = new PropertyPath("SelectedItem." + bindingPath),
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit
            };

            if (control.GetType() == typeof(DatePicker))
            {
                control.SetBinding(DatePicker.SelectedDateProperty, binding);
            }
            else if (control.GetType() == typeof(Slider))
            {
                control.SetBinding(RangeBase.ValueProperty, binding);
            }
            else if (control.GetType() == typeof(TextBox))
            {
                control.SetBinding(TextBox.TextProperty, binding);
            }

            if (typeTextBox == TypeTextBox.IntBox)
            {
                return new SidebarElement(header, control, TypeTextBox.IntBox);
            }

            return new SidebarElement(header, control);
        }

        // sold spec
        private EntityControl GetDiscount(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Процент (%)", "Percent"),
                GetDataGridColumn("Дата начала действия", "DateStart"),
                GetDataGridColumn("Дата окончания действия", "DateEnd")
            };

            Slider slider = new Slider
            {
                Minimum = 0,
                Maximum = 100
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Процент (%):", slider, "Percent"),
                GetSidebarElement("Дата начала действия:", new DatePicker(), "DateStart"),
                GetSidebarElement("Дата окончания действия:", new DatePicker(), "DateEnd"),
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Discount(), dataModel);
        }

        private EntityControl GetDiscountCard(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Кол-во бонусов", "BonusValue")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Кол-во бонусов:", new TextBox(), "BonusValue", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new DiscountCard(), dataModel);
        }

        private EntityControl GetGiftCard(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Номинал", "FaceValue")
            };

            TextBox textBoxRegistrationNumber = new TextBox();
            TextBox textBoxBonusValue = new TextBox();

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Номинал:", new TextBox(), "FaceValue", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new GiftCard(), dataModel);
        }

        // docs
        private EntityControl GetConformityCertificate(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Код ТН ВЭД", "CodeTransportUnion"),
                GetDataGridColumn("Дата регистрации", "RegistrationDate"),
                GetDataGridColumn("Дата окончания действия", "EndDate"),
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Код ТН ВЭД:", new TextBox(), "CodeTransportUnion", TypeTextBox.TextBox),
                GetSidebarElement("Дата регистрации:", new DatePicker(), "RegistrationDate"),
                GetSidebarElement("Дата окончания действия:", new DatePicker(), "EndDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new ConformityCertificate(), dataModel);
        }

        private EntityControl GetConformityDeclaration(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Код ТН ВЭД", "CodeTransportUnion"),
                GetDataGridColumn("Дата регистрации", "RegistrationDate"),
                GetDataGridColumn("Дата окончания действия", "EndDate"),
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Код ТН ВЭД:", new TextBox(), "CodeTransportUnion", TypeTextBox.TextBox),
                GetSidebarElement("Дата регистрации:", new DatePicker(), "RegistrationDate"),
                GetSidebarElement("Дата окончания действия:", new DatePicker(), "EndDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new ConformityDeclaration(), dataModel);
        }

        private EntityControl GetGoodTransportWaybill(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Имя товароотправителя", "Consignor"),
                GetDataGridColumn("Имя клиента", "Customer"),
                GetDataGridColumn("Точка отправления", "LoadingPoint"),
                GetDataGridColumn("Точка прибытия", "ShippingPoint")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Имя товароотправителя:", new TextBox(), "Consignor", TypeTextBox.TextBox),
                GetSidebarElement("Имя клиента:", new TextBox(), "Customer", TypeTextBox.TextBox),
                GetSidebarElement("Точка отправления:", new TextBox(), "LoadingPoint", TypeTextBox.TextBox),
                GetSidebarElement("Точка прибытия:", new TextBox(), "ShippingPoint", TypeTextBox.TextBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new GoodTransportWaybill(), dataModel);
        }

        private EntityControl GetSanitaryEpidemiologicalCertificate(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Имя товароотправителя", "Consignor"),
                GetDataGridColumn("Имя клиента", "Customer"),
                GetDataGridColumn("Дата регистрации", "RegistrationDate")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Имя товароотправителя:", new TextBox(), "Consignor", TypeTextBox.TextBox),
                GetSidebarElement("Имя клиента:", new TextBox(), "Customer", TypeTextBox.TextBox),
                GetSidebarElement("Дата регистрации:", new DatePicker(), "RegistrationDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new SanitaryEpidemiologicalCertificate(), dataModel);
        }

        private EntityControl GetStateRegistrationCertificate(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Регистрационный номер", "RegistrationNumber"),
                GetDataGridColumn("Код ТН ВЭД", "CodeTransportUnion"),
                GetDataGridColumn("Дата регистрации", "RegistrationDate")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Регистрационный номер:", new TextBox(), "RegistrationNumber", TypeTextBox.IntBox),
                GetSidebarElement("Код ТН ВЭД:", new TextBox(), "CodeTransportUnion", TypeTextBox.TextBox),
                GetSidebarElement("Дата регистрации:", new DatePicker(), "RegistrationDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new StateRegistrationCertificate(), dataModel);
        }

        // product editions
        private EntityControl GetElectronicEdition(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("Цена", "Price")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("Цена:", new TextBox(), "Price", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new ElectronicEdition(), dataModel);
        }

        private EntityControl GetPaperEdition(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("Тип обложки", "TypeCover"),
                GetDataGridColumn("Вес", "Weight"),
                GetDataGridColumn("Цена", "Price"),
                GetDataGridColumn("№ товарно-транспортной накладной", "GoodTransportWaybill.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("Тип обложки:", new TextBox(), "TypeCover", TypeTextBox.TextBox),
                GetSidebarElement("Вес:", new TextBox(), "Weight", TypeTextBox.IntBox),
                GetSidebarElement("Цена:", new TextBox(), "Price", TypeTextBox.IntBox),
                GetSidebarElement("№ товарно-транспортной накладной:", new TextBox(), "GoodTransportWaybill.Id", TypeTextBox.IntBox),
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PaperEdition(), dataModel);
        }

        // Product spec
        private EntityControl GetAuthor(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Имя", "Name"),
                GetDataGridColumn("Фамилия", "Surname"),
                GetDataGridColumn("Отчество", "Patronymic")
            };


            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Имя:", new TextBox(), "Name", TypeTextBox.TextBox),
                GetSidebarElement("Фамилия:", new TextBox(), "Surname", TypeTextBox.TextBox),
                GetSidebarElement("Отчество:", new TextBox(), "Patronymic", TypeTextBox.TextBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Author(), dataModel);
        }

        private EntityControl GetGenre(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Название жанра", "Name")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Название жанра:", new TextBox(), "Name", TypeTextBox.TextBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Genre(), dataModel);
        }

        private EntityControl GetPublisher(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Имя издателя", "Name")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Имя издателя:", new TextBox(), "Name", TypeTextBox.TextBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Publisher(), dataModel);
        }

        private EntityControl GetISBN(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("EAN.UCC", "EANUCC"),
                GetDataGridColumn("Номер регистрационной группы", "NumberOfRegistrationGroup"),
                GetDataGridColumn("Номер регистранта", "NumberOfRegistrant"),
                GetDataGridColumn("Номер издания", "NumberOfEdition"),
                GetDataGridColumn("Контрольная цифра", "CheckDigit")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("EAN.UCC:", new TextBox(), "EANUCC", TypeTextBox.IntBox),
                GetSidebarElement("Номер регистрационной группы:", new TextBox(), "NumberOfRegistrationGroup", TypeTextBox.IntBox),
                GetSidebarElement("Номер регистранта:",  new TextBox(), "NumberOfRegistrant", TypeTextBox.IntBox),
                GetSidebarElement("Номер издания:", new TextBox(), "NumberOfEdition", TypeTextBox.IntBox),
                GetSidebarElement("Контрольная цифра:", new TextBox(), "CheckDigit", TypeTextBox.IntBox),
            };

            return new EntityControl(dataGridColumns, sidebarElements, new ISBN(), dataModel);
        }

        private EntityControl GetPrintedMatterAuthor(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ автора", "Author.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ автора:", new TextBox(), "Author.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PrintedMatterAuthor(), dataModel);
        }

        private EntityControl GetPrintedMatterGenre(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ жанра", "Genre.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ жанра:", new TextBox(), "Genre.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PrintedMatterGenre(), dataModel);
        }

        private EntityControl GetPrintedMatterPublisher(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ издания", "Publisher.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ издания:", new TextBox(), "Publisher.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PrintedMatterPublisher(), dataModel);
        }

        private EntityControl GetPrintedMatterISBN(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ ISBN", "ISBN.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ ISBN:", new TextBox(), "ISBN.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PrintedMatterISBN(), dataModel);
        }

        // STATUS SPEC
        private EntityControl GetArrivalPrintedMatter(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("Кол-во товаров", "Count"),
                GetDataGridColumn("Дата прибытия", "ArrivalDate")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("Кол-во товаров:", new TextBox(), "Count", TypeTextBox.IntBox),
                GetSidebarElement("Дата прибытия:", new DatePicker(), "ArrivalDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new ArrivalPrintedMatter(), dataModel);
        }

        private EntityControl GetPriceList(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("Кол-во товаров", "Count")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("Кол-во товаров:", new TextBox(), "Count", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PriceList(), dataModel);
        }

        private EntityControl GetSoldPrintedMatter(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("Кол-во товаров", "Count"),
                GetDataGridColumn("Дата продажи", "SoldDate"),
                GetDataGridColumn("№ скидки", "Discount.Id"),
                GetDataGridColumn("№ скидочной карты", "DiscountCard.Id"),
                GetDataGridColumn("№ подарочной карты", "GiftCard.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("Кол-во товаров:", new TextBox(), "Count", TypeTextBox.IntBox),
                GetSidebarElement("Дата продажи:", new DatePicker(), "SoldDate"),
                GetSidebarElement("№ скидки:", new TextBox(), "Discount.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ скидочной карты:", new TextBox(), "DiscountCard.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ подарочной карты:", new TextBox(), "GiftCard.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new SoldPrintedMatter(), dataModel);
        }

        // types print matt
        private EntityControl GetBrochure(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ декларации о соответствии", "ConformityDeclaration.Id"),
                GetDataGridColumn("№ свидетельства о регистрации", "StateRegistrationCertificate.Id"),
                GetDataGridColumn("№ санитарно-эпидемиологического сертификата", "SanitaryEpidemiologicalCertificate.Id"),
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ декларации о соответствии:", new TextBox(), "ConformityDeclaration.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ свидетельства о регистрации:", new TextBox(), "StateRegistrationCertificate.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ санитарно-эпидемиологического сертификата:", new TextBox(), "SanitaryEpidemiologicalCertificate.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Brochure(), dataModel);
        }

        private EntityControl GetJournal(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ декларации о соответствии", "ConformityDeclaration.Id"),
                GetDataGridColumn("№ свидетельства о регистрации", "StateRegistrationCertificate.Id"),
                GetDataGridColumn("№ санитарно-эпидемиологического сертификата", "SanitaryEpidemiologicalCertificate.Id"),
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ декларации о соответствии:", new TextBox(), "ConformityDeclaration.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ свидетельства о регистрации:", new TextBox(), "StateRegistrationCertificate.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ санитарно-эпидемиологического сертификата:", new TextBox(), "SanitaryEpidemiologicalCertificate.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new Journal(), dataModel);
        }

        private EntityControl GetSchoolBook(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ декларации о соответствии", "ConformityDeclaration.Id"),
                GetDataGridColumn("№ свидетельства о регистрации", "StateRegistrationCertificate.Id"),
                GetDataGridColumn("№ санитарно-эпидемиологического сертификата", "SanitaryEpidemiologicalCertificate.Id"),
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ декларации о соответствии:", new TextBox(), "ConformityDeclaration.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ свидетельства о регистрации:", new TextBox(), "StateRegistrationCertificate.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ санитарно-эпидемиологического сертификата:", new TextBox(), "SanitaryEpidemiologicalCertificate.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new SchoolBook(), dataModel);
        }

        private EntityControl GetStandardBook(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("№ печатного продукта", "PrintedMatter.Id"),
                GetDataGridColumn("№ сертификата о соответствии", "ConformityCertificate.Id")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("№ печатного продукта:", new TextBox(), "PrintedMatter.Id", TypeTextBox.IntBox),
                GetSidebarElement("№ сертификата о соответствии:", new TextBox(), "ConformityCertificate.Id", TypeTextBox.IntBox)
            };

            return new EntityControl(dataGridColumns, sidebarElements, new StandardBook(), dataModel);
        }

        // printedmatter

        private EntityControl GetPrintedMatter(DataModel dataModel)
        {
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>
            {
                GetDataGridColumn("№", "Id"),
                GetDataGridColumn("Заголовок", "Title"),
                GetDataGridColumn("Аннотация", "Annotation"),
                GetDataGridColumn("Кол-во страниц", "NumberOfPages"),
                GetDataGridColumn("Возрастное ограничение", "AgeLimit"),
                GetDataGridColumn("Дата издания", "ImprintDate")
            };

            List<SidebarElement> sidebarElements = new List<SidebarElement>
            {
                GetSidebarElement("Заголовок:", new TextBox(), "Title", TypeTextBox.TextBox),
                GetSidebarElement("Аннотация:", new TextBox(), "Annotation", TypeTextBox.TextBox),
                GetSidebarElement("Кол-во страниц:", new TextBox(), "NumberOfPages", TypeTextBox.IntBox),
                GetSidebarElement("Возрастное ограничение:", new TextBox(), "AgeLimit", TypeTextBox.IntBox),
                GetSidebarElement("Дата издания:", new DatePicker(), "ImprintDate")
            };

            return new EntityControl(dataGridColumns, sidebarElements, new PrintedMatter(), dataModel);
        }
    }
}
using SummerPractice.Db.Context;
using SummerPractice3.Db.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SummerPractice3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //myButton.FontSize = 50;
            //myButton.Content = "Hello there";

            //myTextBlock.Text = "Hello from cs side!";
            //myTextBlock.Foreground = Brushes.BlanchedAlmond;

            //var codeBehindTB = new TextBlock();
            //codeBehindTB.Text = "Hello world!";
            //codeBehindTB.Inlines.Add(" This is added using Inlines!");
            //codeBehindTB.Inlines.Add(new Run(" Run text that I added in Code behind")
            //{
            //    Foreground = Brushes.Red,
            //    TextDecorations = TextDecorations.Underline
            //});
            //codeBehindTB.TextWrapping = TextWrapping.Wrap;


            //this.Content = codeBehindTB;
        }
        private bool isOrderEditing = false;
        private int orderEditingId = -1;

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var processInfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
            processInfo.UseShellExecute = true;
            Process.Start(processInfo);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                
                var list = db.MaterialSuppliers.ToList();
                var suppliers = list;
                ObservableCollection<MaterialSupplier> materialSuppliers = new ObservableCollection<MaterialSupplier>();
                foreach (var supplier in suppliers)
                {
                    materialSuppliers.Add(supplier);
                }
                //label1.Content = suppliers[0].Name;
                mainGrid.ItemsSource = list;
                mainGrid.Columns[0].Visibility = Visibility.Collapsed;
            }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                MaterialSupplier materialSupplier = new MaterialSupplier() { Bank = new Bank() { Address = "Пл. Ленина, 9", Name = "Скамбанк" }, BankAccount = 121200003311, BusinessAddress = "Пл. Ленина, 10", Name = "Готовые материалы", Code = 34228, INN = 1333445567 };
                db.MaterialSuppliers.Add(materialSupplier);
                db.SaveChanges();
            }
        }

        private void ordersCleanButton_Click(object sender, RoutedEventArgs e)
        {
            isOrderEditing = false;

            orderNumberTextBox.Text = "";
            supplierTextBox.Text = "";
            balanceAccountTextBox.Text = "";
            accompanyingCodeTextBox.Text = "";
            accompanyingNumberTextBox.Text = "";
            materialTextBox.Text = "";
            countTextBox.Text = "";
            unitCostTextBox.Text = "";
        }

        private void ordersDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(mainGrid.SelectedIndex>=0)
            {
                var selectedItem = mainGrid.SelectedItem;
                using (MainDbContext db = new MainDbContext())
                {
                    db.Remove(selectedItem);
                    mainGrid.Items.Remove(mainGrid.SelectedItem);
                    db.SaveChanges();
                    OrdersDataTableRefresh();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }

        //private void OrdersRefreshGrid()
        //{
        //    using (MainDbContext db = new MainDbContext())
        //    {

        //        var list = db.StorageUnits.ToList();
        //        var materialSuppliers = new ObservableCollection<StorageUnit>();
        //        foreach (var item in list)
        //        {
        //            materialSuppliers.Add(item);
        //        }

        //        mainGrid.ItemsSource = list;
        //        mainGrid.Columns[0].Visibility = Visibility.Collapsed;
        //    }
        //}

        private void ordersModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedIndex >= 0)
            {
                isOrderEditing = true;
                var selectedItem = (StorageUnit)mainGrid.SelectedItem;
                orderEditingId = selectedItem.Id;

                //orderNumberTextBox.Text = selectedItem.OrderNumber;
                supplierTextBox.Text = selectedItem.MaterialSupplier;
                balanceAccountTextBox.Text = "";
                accompanyingCodeTextBox.Text = "";
                accompanyingNumberTextBox.Text = "";
                materialTextBox.Text = "";
                countTextBox.Text = "";
                unitCostTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }

        private void ordersAddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabItem tabItem = tabControl.SelectedItem as TabItem;
            string header = (string)tabItem.Header;
            if(header == "Ордеры")
            {
                OrdersDataTableRefresh();
            } else
            if (header == "Поставщики")
            {
                SuppliersDataTableRefresh();
            }
        }

        public void OrdersDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {

                var list = db.StorageUnits.ToList();
                var res = new ObservableCollection<StorageUnit>();
                foreach (var item in list)
                {
                    res.Add(item);
                }
                //label1.Content = suppliers[0].Name;
                //ordersGrid.ItemsSource = res;
                mainGrid.ItemsSource = res;
                //ordersGrid.Columns[0].Visibility = Visibility.Collapsed;
            }
        }
        public void SuppliersDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {

                var list = db.MaterialSuppliers.ToList();
                //var res = new ObservableCollection<StorageUnit>();
                //foreach (var item in list)
                //{
                //    res.Add(item);
                //}
                //label1.Content = suppliers[0].Name;
                //suppliersGrid.ItemsSource = list;
                mainGrid.ItemsSource = list;
                //suppliersGrid.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        //private void ordersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //    var item = (StorageUnit)ordersGrid.SelectedItem;
        //    using (MainDbContext db = new MainDbContext())
        //    {

        //        db.StorageUnits.Update(item);
        //        db.SaveChanges();
        //    }
        //}
    }

    public class StorageUnitEnhanced
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public ulong BalanceAccount { get; set; }
        public int AccompanyingDocumentCode { get; set; }
        public int AccompanyingDocumentNumber { get; set; }
        public double Count { get; set; }
        public double UnitCost { get; set; }
        public string MaterialSupplierName { get; set; }
        public string MaterialName { get; set; }
    }
}



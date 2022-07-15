using SummerPractice.Db.Context;
using SummerPractice3.Db.Entities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
                var selectedItem = (StorageUnitEnhanced)mainGrid.SelectedItem;
                using (MainDbContext db = new MainDbContext())
                {
                    db.Remove(db.StorageUnits.First(x => x.Id == selectedItem.Id));
                    //mainGrid.Items.Remove(mainGrid.SelectedItem);
                    db.SaveChanges();
                    OrdersDataTableRefresh();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }
        private void ordersModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedIndex >= 0)
            {
                isOrderEditing = true;
                var selectedItem = (StorageUnitEnhanced)mainGrid.SelectedItem;
                orderEditingId = selectedItem.Id;

                orderNumberTextBox.Text = selectedItem.OrderNumber.ToString();
                supplierTextBox.Text = selectedItem.MaterialSupplierName;
                balanceAccountTextBox.Text = selectedItem.BalanceAccount.ToString();
                accompanyingCodeTextBox.Text = selectedItem.AccompanyingDocumentCode.ToString();
                accompanyingNumberTextBox.Text = selectedItem.AccompanyingDocumentNumber.ToString();
                materialTextBox.Text = selectedItem.MaterialName;
                countTextBox.Text = selectedItem.Count.ToString();
                unitCostTextBox.Text = selectedItem.UnitCost.ToString();
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }
        private void ordersAddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                try
                {
                    StorageUnit storageUnit = new StorageUnit();
                    if(isOrderEditing)
                        storageUnit.Id = orderEditingId;
                    storageUnit.OrderNumber = Int32.Parse(orderNumberTextBox.Text);
                    storageUnit.Date = orderDatePicker.SelectedDate.Value;
                    storageUnit.MaterialSupplierId = db.MaterialSuppliers.First((x) => x.Name == supplierTextBox.Text).Id;
                    storageUnit.BalanceAccount = ulong.Parse(balanceAccountTextBox.Text);
                    storageUnit.AccompanyingDocumentCode = int.Parse(accompanyingCodeTextBox.Text);
                    storageUnit.AccompanyingDocumentNumber = int.Parse(accompanyingNumberTextBox.Text);
                    storageUnit.MaterialId = db.Materials.First(x => x.Name == materialTextBox.Text).Id;
                    storageUnit.Count = int.Parse(countTextBox.Text);
                    storageUnit.UnitCost = double.Parse(unitCostTextBox.Text);
                    if (isOrderEditing)
                    {
                        db.StorageUnits.Update(storageUnit);
                        isOrderEditing = false;
                    }
                    else
                        db.Add(storageUnit);
                    db.SaveChanges();
                    OrdersDataTableRefresh();
                }
                catch
                {
                    MessageBox.Show("Не удалось добавить элемент!");
                }
            }
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
            if (header == "Банки")
            {
                BanksDataTableRefresh();
            }
            if (header == "Материалы")
            {
                MaterialsDataTableRefresh();
            }
            if (header == "Прочее")
            {
                MeasurementUnitsDataTableRefresh();
            }
        }
        public void OrdersDataTableRefreshold()
        {
            using (MainDbContext db = new MainDbContext())
            {

                var list = db.StorageUnits.ToList();
                var res = new ObservableCollection<StorageUnit>();
                foreach (var item in list)
                {
                    res.Add(item);
                }
                mainGrid.ItemsSource = res;
            }
        }
        public void OrdersDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {

                var list = db.StorageUnits.ToList();
                var res = new ObservableCollection<StorageUnitEnhanced>();
                foreach (var item in list)
                {
                    var tableItem = new StorageUnitEnhanced()
                    {
                        AccompanyingDocumentCode = item.AccompanyingDocumentCode,
                        AccompanyingDocumentNumber = item.AccompanyingDocumentNumber,
                        Date = item.Date,
                        Id = item.Id,
                        OrderNumber = item.OrderNumber,
                        Count=item.Count,
                        UnitCost=item.UnitCost,
                        BalanceAccount=item.BalanceAccount,
                        MaterialName = db.Materials.First((x)=>x.Id==item.MaterialId).Name,
                        MaterialSupplierName = db.MaterialSuppliers.First((x)=>x.Id==item.MaterialSupplierId).Name
                    };
                    res.Add(tableItem);
                }
                mainGrid.ItemsSource = res;
            }
        }
        public void SuppliersDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {
                var list = db.MaterialSuppliers.ToList();
                var res = new ObservableCollection<SupplierEnchanced>();
                foreach (var item in list)
                {
                    var tableItem = new SupplierEnchanced()
                    {
                        Id = item.Id,
                        BankAccount = item.BankAccount,
                        BankName = db.Banks.First(x => x.Id == item.BankId).Name,
                        BusinessAddress = item.BusinessAddress,
                        Code = item.Code,
                        INN = item.INN,
                        Name = item.Name
                    };
                    res.Add(tableItem);
                }
                mainGrid.ItemsSource = res;
            }
        }

        private void supplierCleanButton_Click(object sender, RoutedEventArgs e)
        {
            supplierAddressTextBox.Text = "";
            supplierBankAccountTextBox.Text = "";
            supplierBankName.Text = "";
            supplierCodeTextBox.Text = "";
            supplierINNTextBox.Text = "";
            supplierNameTextBox.Text = "";
        }

        private void supplierDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedIndex >= 0)
            {
                var selectedItem = (SupplierEnchanced)mainGrid.SelectedItem;
                using (MainDbContext db = new MainDbContext())
                {
                    db.Remove(db.MaterialSuppliers.First(x => x.Id == selectedItem.Id));
                    db.SaveChanges();
                    SuppliersDataTableRefresh();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }

        private void supplierModifyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void supplierAddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                try
                {
                    var materialSupplier = new MaterialSupplier();
                    //if (isOrderEditing)
                    //storageUnit.Id = orderEditingId;
                    materialSupplier.Code = int.Parse(supplierCodeTextBox.Text);
                    materialSupplier.Name=supplierNameTextBox.Text;
                    materialSupplier.BusinessAddress=supplierAddressTextBox.Text;
                    materialSupplier.BankAccount = ulong.Parse(supplierBankAccountTextBox.Text);
                    materialSupplier.INN=ulong.Parse(supplierINNTextBox.Text);
                    materialSupplier.BankId = db.Banks.First(x => x.Name == supplierBankName.Text).Id;
                    //if (isOrderEditing)
                    //{
                    //    db.StorageUnits.Update(materialSupplier);
                    //    isOrderEditing = false;
                    //}
                    //else
                        db.Add(materialSupplier);
                    db.SaveChanges();
                    SuppliersDataTableRefresh();
                }
                catch
                {
                    MessageBox.Show("Не удалось добавить элемент!");
                }
            }
        }

        private void banksAddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                try
                {
                    var bank = new Bank();
                    bank.Address = bankAddressTextBox.Text;
                    bank.Name=bankNameTextBox.Text;

                    db.Add(bank);
                    db.SaveChanges();
                    BanksDataTableRefresh();
                }
                catch
                {
                    MessageBox.Show("Не удалось добавить элемент!");
                }
            }
        }

        private void banksDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedIndex >= 0)
            {
                var selectedItem = (Bank)mainGrid.SelectedItem;
                using (MainDbContext db = new MainDbContext())
                {
                    db.Remove(db.Banks.First(x => x.Id == selectedItem.Id));
                    db.SaveChanges();
                    BanksDataTableRefresh();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }

        private void BanksDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {
                var list = db.Banks.ToList();
                var res = new ObservableCollection<Bank>();
                foreach (var item in list)
                {
                    res.Add(item);
                }
                mainGrid.ItemsSource = res;
                mainGrid.Columns[2].Visibility = Visibility.Collapsed;
                mainGrid.Columns[4].Visibility = Visibility.Collapsed;
            }
        }

        private void banksCleanButton_Click(object sender, RoutedEventArgs e)
        {
            bankAddressTextBox.Text = string.Empty;
            bankNameTextBox.Text = string.Empty;
        }

        private void materialsCleanButton_Click(object sender, RoutedEventArgs e)
        {
            materialClassCodeTextBox.Text = string.Empty;
            materialNameTextBox.Text = string.Empty;
            materialGroupCodeTextBox.Text = string.Empty;
            materialMeasurementUnitNameTextBox.Text = string.Empty;
        }

        private void materialsDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedIndex >= 0)
            {
                var selectedItem = (MaterialEnchanced)mainGrid.SelectedItem;
                using (MainDbContext db = new MainDbContext())
                {
                    db.Remove(db.Materials.First(x => x.Id == selectedItem.Id));
                    db.SaveChanges();
                    MaterialsDataTableRefresh();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элемент");
            }
        }

        private void materialsAddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                try
                {
                    var material = new Material();
                    material.Name = materialNameTextBox.Text;
                    material.ClassCode = int.Parse(materialClassCodeTextBox.Text);
                    material.GroupCode = int.Parse(materialGroupCodeTextBox.Text);
                    material.MeasurementUnitId = db.MeasurementUnits.First(x => x.UnitName == materialMeasurementUnitNameTextBox.Text).Id;

                    db.Add(material);
                    db.SaveChanges();
                    MaterialsDataTableRefresh();
                }
                catch
                {
                    MessageBox.Show("Не удалось добавить элемент!");
                }
            }
        }
        private void MaterialsDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {
                var list = db.Materials.ToList();
                var res = new ObservableCollection<MaterialEnchanced>();
                foreach (var item in list)
                {
                    var tableItem = new MaterialEnchanced()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ClassCode = item.ClassCode,
                        GroupCode = item.GroupCode,
                        MeasurementUnitName = db.MeasurementUnits.First(x => x.Id == item.MeasurementUnitId).UnitName
                    };
                    res.Add(tableItem);
                }
                mainGrid.ItemsSource = res;
            }
        }
        private void MeasurementUnitsDataTableRefresh()
        {
            using (MainDbContext db = new MainDbContext())
            {
                var list = db.MeasurementUnits.ToList();
                mainGrid.ItemsSource = list;
                mainGrid.Columns[1].Visibility = Visibility.Collapsed;
                mainGrid.Columns[3].Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                var result = db.StorageUnits.Join(db.Materials, u => u.MaterialId, c => c.Id, (u, c) => new { MaterialName = c.Name, MaterialSupplierId = u.MaterialSupplierId }).Join(db.MaterialSuppliers, m => m.MaterialSupplierId, v => v.Id, (m, v) => new { MaterialSupplierName = v.Name });
                var list = result.Distinct().ToList();
                otherGrid1.ItemsSource = list;
                task1Res.Content = list.Count + " поставщиков";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (MainDbContext db = new MainDbContext())
            {
                try
                {
                    var result = db.Banks.Where(x => x.Address == bankAddressToSuppliers.Text).Join(db.MaterialSuppliers, u => u.Id, c => c.BankId, (u, c) => new { MaterialSupplierName = c.Name }).Distinct().ToList().Count;
                    task2Res.Content = result + " поставщиков";
                }
                catch
                {
                    MessageBox.Show("Нет банка с таким адресом!");
                }

            }
            
        }
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
    public class SupplierEnchanced
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public ulong INN { get; set; }
        public string BusinessAddress { get; set; }
        public ulong BankAccount { get; set; }
        public string BankName { get; set; }
    }
    public class MaterialEnchanced
    {
        public int Id { get; set; }
        public int ClassCode { get; set; }
        public int GroupCode { get; set; }
        public string Name { get; set; }
        public string MeasurementUnitName { get; set; }
    }
}



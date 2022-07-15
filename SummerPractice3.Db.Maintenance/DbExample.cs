using SummerPractice.Db.Context;
using SummerPractice3.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice3.Db.Maintenance
{
    public static class DbExample
    {
        public static void Populate()
        {
            using (MainDbContext db = new MainDbContext(true))
            {
                
                //Add Banks
                List<Bank> banks = new List<Bank>();
                banks.Add(new Bank() { Address = "ул. Семилуксая, 9", Name = "Почта-Банк" });
                banks.Add(new Bank() { Address = "ул. Запорожняя, 14", Name = "Вещь-банк" });
                banks.Add(new Bank() { Address = "ул. Шишкова, 1а", Name = "Бета страхование" });
                banks.Add(new Bank() { Address = "Пл. Ленина, 9", Name = "Скамбанк" });
                db.Banks.AddRange(banks);

                //Add Measurement Units
                var measurementUnits = new List<MeasurementUnit>();
                measurementUnits.Add(new MeasurementUnit() { UnitName = "см" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "м" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "м^2" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "м^3" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "кг" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "т" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "шт" });
                measurementUnits.Add(new MeasurementUnit() { UnitName = "л" });
                db.MeasurementUnits.AddRange(measurementUnits);
                db.SaveChanges();

                //Add Materials
                var materials = new List<Material>();
                materials.Add(new Material()
                {
                    Name = "Пакет мусорный",
                    MeasurementUnitId = db.MeasurementUnits.First((x) => x.UnitName == "шт").Id,
                    ClassCode = 1233,
                    GroupCode = 12
                });
                materials.Add(new Material()
                {
                    Name = "Балка деревянная",
                    MeasurementUnitId = db.MeasurementUnits.First((x) => x.UnitName == "м").Id,
                    ClassCode = 122,
                    GroupCode = 4
                });
                materials.Add(new Material()
                {
                    Name = "Бкнзин АИ92",
                    MeasurementUnitId = db.MeasurementUnits.First((x) => x.UnitName == "м^2").Id,
                    ClassCode = 2,
                    GroupCode = 7
                });
                materials.Add(new Material()
                {
                    Name = "Керосин",
                    MeasurementUnitId = db.MeasurementUnits.First((x) => x.UnitName == "м^3").Id,
                    ClassCode = 2,
                    GroupCode = 9
                });
                db.Materials.AddRange(materials);

                //Add Material Suppliers
                var materialSuppliers = new List<MaterialSupplier>();
                materialSuppliers.Add(new MaterialSupplier()
                {
                    BankAccount = 132433114123,
                    BusinessAddress = "ул. Дзержинского, 2к4",
                    Name = "Материалы даром",
                    Code = 34212,
                    INN = 7459300989,
                    BankId = db.Banks.First((x) => x.Name == "Бета страхование").Id
                });
                materialSuppliers.Add(new MaterialSupplier()
                {
                    BankAccount = 121200003311,
                    BusinessAddress = "Пл. Ленина, 10",
                    Name = "Готовые материалы",
                    Code = 34228,
                    INN = 1333445567,
                    BankId = db.Banks.First((x) => x.Name == "Скамбанк").Id
                });
                materialSuppliers.Add(new MaterialSupplier()
                {
                    BankAccount = 121205003912,
                    BusinessAddress = "ул. Дешевого, 44",
                    Name = "Вещи с Китая",
                    Code = 34435,
                    INN = 1333490998,
                    BankId = db.Banks.First((x) => x.Name == "Скамбанк").Id
                });
                db.MaterialSuppliers.AddRange(materialSuppliers);
                db.SaveChanges();

                //Add Storage Units
                var storageUnits = new List<StorageUnit>();
                storageUnits.Add(new StorageUnit()
                {
                    AccompanyingDocumentCode = 12,
                    AccompanyingDocumentNumber = 3332,
                    BalanceAccount = 21274390,
                    Count = 400,
                    Date = new(2022, 4, 29),
                    OrderNumber = 143556,
                    UnitCost = 45,
                    MaterialId = db.Materials.First((x) => x.Name == "Пакет мусорный").Id,
                    MaterialSupplierId = db.MaterialSuppliers.First((x) => x.Name == "Материалы даром").Id
                });
                storageUnits.Add(new StorageUnit()
                {
                    AccompanyingDocumentCode = 12,
                    AccompanyingDocumentNumber = 334,
                    BalanceAccount = 21274395,
                    Count = 1200,
                    Date = new(2022, 2, 24),
                    OrderNumber = 143557,
                    UnitCost = 37,
                    MaterialId = db.Materials.First((x) => x.Name == "Пакет мусорный").Id,
                    MaterialSupplierId = db.MaterialSuppliers.First((x) => x.Name == "Вещи с Китая").Id
                });
                storageUnits.Add(new StorageUnit()
                {
                    AccompanyingDocumentCode = 10,
                    AccompanyingDocumentNumber = 4338,
                    BalanceAccount = 21274451,
                    Count = 20,
                    Date = new(2021, 7, 10),
                    OrderNumber = 143558,
                    UnitCost = 150,
                    MaterialId = db.Materials.First((x) => x.Name == "Балка деревянная").Id,
                    MaterialSupplierId = db.MaterialSuppliers.First((x) => x.Name == "Материалы даром").Id
                });
                db.StorageUnits.AddRange(storageUnits);

                db.SaveChanges();
            }
        }
    }
}

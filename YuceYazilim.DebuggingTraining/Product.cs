using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace YuceYazilim.DebuggingTraining
{
    [DebuggerDisplay("Ürün Adı: {ProductName} - Kategori Id: {CategoryId},")]
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}

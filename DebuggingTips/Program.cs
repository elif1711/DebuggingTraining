using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using YuceYazilim.DebuggingTips;

namespace DebuggingTips
{
    class Program
    {
        static void Main()
        {
            SomeMethods methods = new SomeMethods(); ;

            //methods.GetTheNameStepByStep();
            
            List<Product> liste = methods.GetAllProducts();
            
            ////methods.JsonForTextVisualiazer();

            //methods.BreakWhenValueChange();            

            //decimal value = methods.GetUnitsInStock();

            bool val = methods.ChangeValueRunTime(15);

            if (val)
            {
                Console.WriteLine("return true");
            }
        }
    }

    public class SomeMethods
    {
        public decimal newValue = 0;
        public void GetTheNameStepByStep()
        {
            char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
            string name = "";
            int[] a = new int[10];
            for (int i = 0; i < letters.Length; i++)
            {
                name += letters[i];
                a[i] = i + 1;
                SendMessage(name, a[i]);
            }
            Console.ReadKey();
        }

        public void SendMessage(string name, int msg)
        {
            Console.WriteLine("Hello, " + name + "! Count to " + msg);
        }

        public List<Product> GetAllProducts()
        {
            using (var ctx = new NorthwindContext())
            {
                List<Product> products = ctx.Products.ToList();
                List<Product> addedProducts = new List<Product>();

                foreach (var item in products)
                {
                    if (item.UnitPrice > 30)
                    {
                        addedProducts.Add(item);
                    }
                }

                return ctx.Products.ToList();
            }
        }
        
        public decimal GetUnitsInStock()
        {
            using (var context = new NorthwindContext())
            {
                decimal unitPrice = context.Products.Where(x => x.ProductName.Contains("Chai")).Select(x => x.UnitPrice).FirstOrDefault();
                return unitPrice;
            }

        }
        
        public bool ChangeValueRunTime(decimal value)
        {
            if (value < 5)
            {
                newValue += 10;
                return true;
            }

            return false;
        }

        public string JsonForTextVisualiazer()
        {
            using (StreamReader sr = new StreamReader(@"/product.json"))
            {
                string json = sr.ReadToEnd();
                return json;
            }
        }

        public Product BreakWhenValueChange()
        {
            using (var context = new NorthwindContext())
            {
                Product product = context.Products.Where(x => x.CategoryId == 1).FirstOrDefault();
                if (product.UnitPrice > 1)
                {
                    product.QuantityPerUnit = "change quantity per unit";
                }

                return product;
            } 
        }

    }
}

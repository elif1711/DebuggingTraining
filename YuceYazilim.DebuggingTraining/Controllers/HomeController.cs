using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace YuceYazilim.DebuggingTraining.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly NorthwindContext _context;
        
        decimal newValue;
        public HomeController(NorthwindContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<Product> liste = _context.Products.ToList();

            List<Product> liste2 = new List<Product>();

            foreach (var item in liste)
            {
                liste2.Add(item);
            }
            return Ok(liste);
        }

        [HttpGet]
        public IActionResult MethodOrderForCallStack()
        {
            ThirdMethod();
            return Ok();
        }

        private string ThirdMethod()
        {
            SecondMethod();
            return "Üçüncü metot çalıştı";
        }

        private string SecondMethod()
        {
            FirstMethod();
            return "İkinci metot çalıştı";
        }

        private string FirstMethod()
        {
            return "Birinci metot çalıştı";
        }
        
        [HttpGet]
        public IActionResult ReadFromJsonFile()
        {
            List<Product> products;
            using (StreamReader sr = new StreamReader(@"/product.json"))
            {
                string json = sr.ReadToEnd();
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            return Ok(products);
        }

        [HttpGet]
        public IActionResult LetsGetAnException()
        {
            List<Category> list = _context.Categories.ToList();
            WriteToFileAsJson(list);
            return Ok();
        }

        [HttpGet]
        public IActionResult ChangeUnitsInStock()
        {
            Product product = _context.Products.Where(x => x.ProductName.Contains("Chai")).FirstOrDefault();

            for (short i = 5; i <= 20; i += 5)
            {
                product.UnitsInStock += i;
            }
            return Ok(product);
        }

        public void WriteToFileAsJson(List<Category> categories)
        {
            using (StreamWriter sr = new StreamWriter(@"/Users/Asus/Desktop/deneme.txt"))
            {
                for (int i = 0; i <= categories.Count - 1; i++)
                {
                    categories[i].Picture = null;
                    sr.WriteLine(JsonConvert.SerializeObject(categories[i]));
                }
            }
        }

        public decimal GetUnitsInStock()
        {
            decimal unitPrice = _context.Products.Where(x => x.ProductName.Contains("Chai")).Select(x => x.UnitPrice).FirstOrDefault();
            return unitPrice;
        }

        [HttpGet]
        public IActionResult SeeWhatHappensOnAutos()
        {
            decimal value = GetUnitsInStock();
            
            if (value >= 5)
            {
                newValue = value + 10;
            }

            return Ok();
        }

    }
}

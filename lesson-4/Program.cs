using System;
using System.Text.Json.Serialization;

using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace lesson_4
{
    class Program
    {

      

        class Product : Desk
        {
            public string Name { get; set; }
            public DateTime Expiry { get; set; }
            public Decimal Price { get; set; }
            public string[] Sizes { get; set; }
        }

        class OtherProduct : Desk
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
        }

        abstract class Desk { }


        static void Main(string[] args)
        {
            


            string way = Environment.CurrentDirectory;

            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Price = 3.99M;
            product.Sizes = new string[] { "Small", "Medium", "Large" };

            string jsonProduct = JsonConvert.SerializeObject(product);
            File.WriteAllText(way+"/1.json", jsonProduct);

            

            OtherProduct otherProduct = new OtherProduct();
            otherProduct.Name = "Car";
            otherProduct.Size = 10;
            otherProduct.Height = 20;
            otherProduct.Width = 30;

            string jsonOtherProduct = JsonConvert.SerializeObject(otherProduct);
            File.WriteAllText(way + "/2.json", jsonOtherProduct);

            

            string jsonReadOtherProduct = File.ReadAllText(way + "/2.json");
            string jsonReadProduct = File.ReadAllText(way + "/1.json");


            var deserializedProduct = JsonConvert.DeserializeObject<Product>(jsonReadProduct);
            var deserializedOtherProduct = JsonConvert.DeserializeObject<OtherProduct>(jsonReadOtherProduct);

            Console.WriteLine(deserializedProduct.Name);
            Console.WriteLine(deserializedOtherProduct.Name);


            
            List<Desk> productList = new List<Desk>();
            productList.Add(deserializedProduct);
            productList.Add(deserializedOtherProduct);

            string jsonList = JsonConvert.SerializeObject(productList);
   
            File.WriteAllText(way + "/3.json", jsonList);


            string jsonReadList = File.ReadAllText(way + "/3.json");

            var deserializedList = JsonConvert.DeserializeObject<List<object>>(jsonReadList);

            foreach(var item in deserializedList)
            {
                Console.WriteLine(item);
            }       
            Console.ReadLine();      
        }

    }
}

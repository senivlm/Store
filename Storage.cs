using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    class Storage : ProductCollection
    {
        public Storage(params Product[] products)
            : base(products)
        { }

        public Storage(IEnumerable<Product> products)
            : base(products)
        { }

        public void GetProductFromConsole()
        {
            try
            {
                QualityGrade quality = QualityGrade.Highest;
                MeatType meatType = MeatType.Beef;
                int consumeIn = 0;
                bool isMeat = false, isDairy = false;

                Console.WriteLine("Type the product name and press Enter:");
                string name = Console.ReadLine();

                Console.WriteLine("Type the product price as floating point value:");
                double price = double.Parse(Console.ReadLine());

                Console.WriteLine("Type the product weight as floating point value:");
                double weight = double.Parse(Console.ReadLine());

                Console.WriteLine("Type the consume time in days(integer):");
                consumeIn = int.Parse(Console.ReadLine());

                Console.WriteLine("Is the product supposed to be Meat or Dairy (Meat/Dairy/blank + Enter):");
                switch (Console.ReadLine())
                {
                    case "Meat":
                        Console.WriteLine("Type the meat quality (Highest/First/Second):");
                        quality = Enum.Parse<QualityGrade>(Console.ReadLine());

                        Console.WriteLine("Type the meat type (Mutton/Beef/Pork/Chicken):");
                        meatType = Enum.Parse<MeatType>(Console.ReadLine());

                        isMeat = true;
                        break;

                    case "Dairy":
                        isDairy = true;
                        break;
                }

                if (isMeat) Add(new Meat(name, price, weight, consumeIn, quality, meatType));
                else if (isDairy) Add(new Dairy(name, price, weight, consumeIn));
                else Add(new Product(name, price, weight, consumeIn));
            }
            catch (FormatException)
            {
                Console.WriteLine("Format seems to be wrong, product was not added");
            }
        }

        public void GetProductsFromConsole()
        {
            GetProductFromConsole();
            Console.WriteLine("Would you like to add another product (yes/blank + enter):");
            if (Console.ReadLine() == "yes") GetProductFromConsole();
        }

        public void GetProductsFromFile(string filePath)
        {
            try
            {
                foreach (string spr in System.IO.File.ReadAllLines(filePath))
                {
                    if (Product.TryParse(spr, out Product pr)) Add(pr);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveSpoiledAndRecordToFile(string filePath)
        {
            var spoiled = new ProductCollection();
            foreach (var item in this)
                if (!item.Key.IsFresh)
                {
                    spoiled.Add(item.Key, item.Value);
                    RemoveAll(item.Key);
                }

            System.IO.File.WriteAllText(filePath, spoiled.ToString());
        }

        public List<Meat> GetMeatList()
        {
            var res = new List<Meat>();
            foreach (Product item in Products)
            {
                if (item is Meat) res.Add(item as Meat);
            }
            return res;
        }

        public void MultPrice(double m)
        {
            foreach (Product item in Products)
            {
                item.MultPrice(m);
            }
        }

        public Product this[int i] => Products.ElementAt(i);
    }
}

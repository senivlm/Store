using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    class Storage : ProductCollection
    {
        public Storage(params Product[] products) =>
            Add(products);

        public void GetProductsFromConsole()
        {
            bool proceed;
            do
            {
                proceed = false;

                QualityGrade quality = QualityGrade.Highest;
                MeatType meatType = MeatType.Beef;
                uint consumeIn = 0;
                bool isMeat = false, isDairy = false;

                Console.WriteLine("Type product name and press Enter:");
                string name = Console.ReadLine();

                Console.WriteLine("Type product price as floating point value:");
                if (!double.TryParse(Console.ReadLine(), out double price))
                    throw new FormatException("Wrong format for price");

                Console.WriteLine("Type product weight as floating point value:");
                if (!double.TryParse(Console.ReadLine(), out double weight))
                    throw new FormatException("Wrong format for weight");

                Console.WriteLine("Is the product supposed to be Meat or Dairy (Meat/Dairy/blank + Enter):");
                switch (Console.ReadLine())
                {
                    case "Meat":
                        Console.WriteLine("Type meat quality (Highest/First/Second):");
                        switch (Console.ReadLine())
                        {
                            case "Highest":
                                quality = QualityGrade.Highest;
                                break;

                            case "First":
                                quality = QualityGrade.First;
                                break;

                            case "Second":
                                quality = QualityGrade.Second;
                                break;

                            default:
                                throw new FormatException("Wrong format for MeatQuality");
                        }

                        Console.WriteLine("Type meat type (Mutton/Beef/Pork/Chicken):");
                        switch (Console.ReadLine())
                        {
                            case "Mutton":
                                meatType = MeatType.Mutton;
                                break;

                            case "Beef":
                                meatType = MeatType.Beef;
                                break;

                            case "Pork":
                                meatType = MeatType.Pork;
                                break;

                            case "Chicken":
                                meatType = MeatType.Chicken;
                                break;

                            default:
                                throw new FormatException("Wrong format for MeatQuality");
                        }

                        isMeat = true;
                        break;

                    case "Dairy":
                        Console.WriteLine("Type dairy consume time in days(integer):");
                        if (!uint.TryParse(Console.ReadLine(), out consumeIn))
                            throw new FormatException("Wrong format for days");

                        isDairy = true;
                        break;
                }

                if (isMeat) Add(new Meat(name, price, weight, quality, meatType));
                else if (isDairy) Add(new Dairy(name, price, weight, consumeIn));
                else Add(new Product(name, price, weight));

                Console.WriteLine("Would you like to add another product (yes/blank + enter):");
                if (Console.ReadLine() == "yes") proceed = true;
            }
            while (proceed);
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

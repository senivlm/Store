using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class Storage : Products
    {
        public Storage(params Product[] products)
            : base(products)
        { }

        public Storage(IEnumerable<Product> products)
            : base(products)
        { }

        public event EventHandler<GetProductFromConsoleEventArgs> OnGetFromConsoleFail;

        public event EventHandler OnDisplay;

        public void GetProductFromConsole()
        {
            GetProductFromConsoleEventArgs args = new();

            Console.WriteLine("Type the product name and press Enter:");
            args.Name = Console.ReadLine();

            Console.WriteLine("Type the product price as floating point value:");
            args.Price = Console.ReadLine();

            Console.WriteLine("Type the product weight as floating point value:");
            args.Weight = Console.ReadLine();

            Console.WriteLine("Type the consume time in days(integer):");
            args.ConsumeIn = Console.ReadLine();

            Console.WriteLine("Is the product supposed to be Meat or Dairy (Meat/Dairy/blank + Enter):");

            try
            {
                switch (Console.ReadLine())
                {
                    case "Meat":
                        Console.WriteLine("Type the meat quality (Highest/First/Second):");
                        args.Quality = Console.ReadLine();

                        Console.WriteLine("Type the meat type (Mutton/Beef/Pork/Chicken):");
                        args.MeatType = Console.ReadLine();

                        Add(new Meat(
                            args.Name,
                            double.Parse(args.Price),
                            double.Parse(args.Weight),
                            int.Parse(args.ConsumeIn),
                            Enum.Parse<QualityGrade>(args.Quality),
                            Enum.Parse<MeatType>(args.MeatType)
                            ));
                        break;

                    case "Dairy":
                        Add(new Dairy(
                            args.Name,
                            double.Parse(args.Price),
                            double.Parse(args.Weight),
                            int.Parse(args.ConsumeIn)
                            ));
                        break;

                    default:
                        Add(new Product(
                            args.Name,
                            double.Parse(args.Price),
                            double.Parse(args.Weight),
                            int.Parse(args.ConsumeIn)
                            ));
                        break;
                }
            }
            catch (FormatException)
            {
                //Console.WriteLine("Format seems to be wrong, product was not added");
                OnGetFromConsoleFail?.Invoke(this, args);
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
                    if (Meat.TryParse(spr, out Meat mt)) Add(mt);
                    else if (Product.TryParse(spr, out Product pr)) Add(pr);
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
            var spoiled = new Products();
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
            foreach (Product item in UniqueProducts)
            {
                if (item is Meat) res.Add(item as Meat);
            }
            return res;
        }

        public void MultPrice(double m)
        {
            foreach (Product item in UniqueProducts)
            {
                item.MultPrice(m);
            }
        }

        public Product this[int i] => UniqueProducts.ElementAt(i);

        public void Remove(string name)
        {
            foreach (Product p in UniqueProducts)
            {
                if (p.Name == name)
                {
                    Remove(p);
                    break;
                }
            }
        }

        public Product Find(Func<Product, bool> predicate)
            => UniqueProducts.First<Product>(predicate);

        public IEnumerable<Product> FindAll(Func<Product, bool> predicate)
            => UniqueProducts.Where(predicate);

        public IEnumerable<Product> FindSpoiled()
        {
            IEnumerable<Product> result = FindAll(product => !product.IsFresh);
            return result;
        }

        public struct GetProductFromConsoleEventArgs
        {
            public string Name, TypeOfProduct, Price, Weight, ConsumeIn, Quality, MeatType;
        }

        public override string ToString()
        {
            OnDisplay?.Invoke(this, new());
            return base.ToString();
        }
    }
}

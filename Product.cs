using System;
using System.Text.RegularExpressions;

namespace Store
{
    class Product : IEquatable<Product>
    {
        double _price;
        double _weight;
        TimeSpan _consumeIn;

        /// <param name="name">name of the product</param>
        /// <param name="price">price in dollars</param>
        /// <param name="weight">weight in kg</param>
        public Product(string name, double price, double weight, int consumeIn)
        {
            Name = name; Price = price; Weight = weight; ConsumeIn = consumeIn;
            ProductionDate = DateTime.Now;
        }

        public Product() : this("ProductName", 0, 0, 0)
        {
        }

        public string Name { get; private set; }

        public double Price
        {
            get => _price;
            private set
            {
                if (value < 0) throw new FormatException("Price should'n be negative");
                _price = value;
            }
        }

        public double Weight
        {
            get => _weight;
            private set
            {
                if (value < 0) throw new FormatException("Weight should'n be negative");
                _weight = value;
            }
        }

        public int ConsumeIn
        {
            get => (int)_consumeIn.TotalDays;
            private set => _consumeIn = new TimeSpan(value, 0, 0, 0);
        }

        public DateTime ProductionDate { get; private set; }

        public bool IsFresh => DateTime.Now - ProductionDate < _consumeIn;

        public static Product Parse(string s)
        {
            var match = Regex.Match(s,
                @"Name: (\w+), Price: \$(\d+(?:.\d+)?), Weight: (\d+(?:.\d+)?), Consume in (\d+) days");
            if (!match.Success) throw new FormatException("Format seems to be wrong");

            var args = match.Groups;
            return new Product(
                args[1].Value,
                double.Parse(args[2].Value),
                double.Parse(args[3].Value),
                int.Parse(args[4].Value)
            );
        }

        public static bool TryParse(string s, out Product pr)
        {
            try
            {
                pr = Parse(s);
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Failed to parse a product");
            pr = new Product();
            return false;
        }

        public static bool operator ==(Product pr1, Product pr2) => pr1.Equals(pr2);

        public static bool operator !=(Product pr1, Product pr2) => !pr1.Equals(pr2);

        virtual public void MultPrice(double multiplier) => Price *= multiplier;

        public bool Equals(Product other) =>
            Name == other.Name &&
            _price == other._price &&
            _weight == other._weight &&
            _consumeIn == other._consumeIn;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return Equals(obj as Product);
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() =>
            $"Name: {Name}, Price: {_price:c}, Weight: {_weight}, Consume in {ConsumeIn} days";
    }
}

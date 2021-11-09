using System;
using System.Text.RegularExpressions;

namespace Store
{
    public class Product : IEquatable<Product>
    {
        public readonly string Name;
        public readonly DateTime ProductionDate;
        double _price;
        double _weight;
        TimeSpan _consumeIn;

        public Product(string name, double price, double weight, int consumeIn)
        {
            Name = name; Price = price; Weight = weight; ConsumeIn = consumeIn;
            ProductionDate = DateTime.Now;
        }

        public Product(string name, double price, double weight, int consumeIn, DateTime productionDate)
            : this(name, price, weight, consumeIn)
        {
            ProductionDate = productionDate;
        }

        public Product() : this("n/a", 0, 0, 0)
        { }

        public double Price
        {
            get => _price;
            private set
            {
                if (value < 0) throw new FormatException("Price can't be negative");
                _price = value;
            }
        }

        public double Weight
        {
            get => _weight;
            private set
            {
                if (value < 0) throw new FormatException("Weight can't be negative");
                _weight = value;
            }
        }

        public int ConsumeIn
        {
            get => (int)_consumeIn.TotalDays;
            private set
            {
                if (value < 0) throw new FormatException("Consumption time can't be negative");
                _consumeIn = new TimeSpan(value, 0, 0, 0);
            }
        }

        public bool IsFresh => DateTime.Now - ProductionDate < _consumeIn;

        public static Product Parse(string s)
        {
            var match = Regex.Match(s,
                @"(\w+), \$(\d+(?:.\d+)?), (\d+(?:.\d+)?)kg, consume in (\d+) days, produced on (\d{2}/\d{2}/\d{4})");
            if (!match.Success) throw new FormatException("Format seems to be wrong");

            var groups = match.Groups;
            return new Product(
                groups[1].Value,
                double.Parse(groups[2].Value),
                double.Parse(groups[3].Value),
                int.Parse(groups[4].Value),
                DateTime.Parse(groups[5].Value)
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

        public virtual void MultPrice(double multiplier) => Price *= multiplier;

        public bool Equals(Product other) =>
            Name == other.Name &&
            _price == other._price &&
            _weight == other._weight &&
            _consumeIn == other._consumeIn &&
            ProductionDate == other.ProductionDate;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return Equals(obj as Product);
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() =>
            $"{Name}, {_price:c}, {_weight}kg, consume in {ConsumeIn} days, produced on {ProductionDate.ToShortDateString()}";
    }
}

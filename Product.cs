using System;

namespace Store
{
    class Product : IEquatable<Product>
    {
        public string Name { get; set; }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0) throw new FormatException("Price should'n be negative");
                _price = value;
            }
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if (value < 0) throw new FormatException("Weight should'n be negative");
                _weight = value;
            }
        }

        /// <param name="name">name of the product</param>
        /// <param name="price">price in dollars</param>
        /// <param name="weight">weight in kg</param>
        public Product(string name, double price, double weight)
        {
            Name = name;
            _price = price;
            _weight = weight;
        }

        public Product() : this("ProductName", 0, 0)
        {
        }

        virtual public void MultPrice(double multiplier) => Price *= multiplier;

        public bool Equals(Product other) =>
            Name == other.Name && _price == other._price && _weight == other._weight;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return Equals(obj as Product);
        }

        public override int GetHashCode() => (int)(Name.GetHashCode() * (_price + .5) * (_weight + .5));

        public static bool operator ==(Product pr1, Product pr2) => pr1.Equals(pr2);

        public static bool operator !=(Product pr1, Product pr2) => !pr1.Equals(pr2);

        public override string ToString() => $"Name: {Name}, Price: {_price:c}, Weight: {_weight}";

        double _price;
        double _weight;
    }
}

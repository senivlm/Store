using System;

namespace Store
{
    enum QualityGrade { Highest, First, Second }

    enum MeatType { Mutton, Beef, Pork, Chicken }

    class Meat : Product, IEquatable<Meat>
    {
        public QualityGrade Quality { get; set; }

        public MeatType Type { get; set; }

        public Meat(string name, double price, double weight, QualityGrade quality, MeatType type)
            : base(name, price, weight)
        {
            Quality = quality;
            Type = type;
        }

        public Meat() : this("ProductName", 0, 0, 0, 0)
        {
        }

        public override void MultPrice(double multiplier) =>
            Price *= (0.95 * multiplier - 0.05 * (int)Quality);

        public bool Equals(Meat pr) =>
            base.Equals(pr as Product) && Quality == pr.Quality && Type == pr.Type;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return base.Equals(obj as Meat);
        }

        public override int GetHashCode() =>
            base.GetHashCode() * (int)Quality * (int)Type;

        public static bool operator ==(Meat pr1, Meat pr2) => pr1.Equals(pr2);

        public static bool operator !=(Meat pr1, Meat pr2) => !pr1.Equals(pr2);

        public override string ToString() =>
            $"{base.ToString()}, Quality: {Quality}, Type: {Type}";
    }
}

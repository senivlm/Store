using System;

namespace Store
{
    class Dairy : Product, IEquatable<Dairy>
    {
        public uint ConsumeIn { get; set; }

        public Dairy(string name, double price, double weight, uint consumeIn)
            : base(name, price, weight)
        {
            ConsumeIn = consumeIn;
        }

        public Dairy() : this("ProductName", 0, 0, 10)
        {
        }

        public override void MultPrice(double multiplier) =>
            Price *= (0.95 * multiplier + 0.05 * ConsumeIn);

        public bool Equals(Dairy pr) =>
            base.Equals(pr as Product) && ConsumeIn == pr.ConsumeIn;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return Equals(obj as Dairy);
        }

        public override int GetHashCode() => base.GetHashCode() + (int)ConsumeIn;

        public static bool operator ==(Dairy pr1, Dairy pr2) => pr1.Equals(pr2);

        public static bool operator !=(Dairy pr1, Dairy pr2) => !pr1.Equals(pr2);

        public override string ToString() => $"{base.ToString()}, Consume in {ConsumeIn} days";
    }
}

using System;

namespace Store
{
    class Dairy : Product
    {
        public Dairy(string name, double price, double weight, int consumeIn)
            : base(name, price, weight, consumeIn)
        {
        }

        public Dairy() : base()
        {
        }

        public override void MultPrice(double multiplier) =>
            base.MultPrice(0.95 * multiplier + 0.05 * ConsumeIn);
    }
}

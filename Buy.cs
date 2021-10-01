namespace Store
{
    class Buy : ProductCollection
    {
        public double Price
        {
            get
            {
                double sum = 0;
                foreach (var item in this)
                {
                    sum += item.Value * item.Key.Price;
                }
                return sum;
            }
        }

        public double Weight
        {
            get
            {
                double sum = 0;
                foreach (var item in this)
                {
                    sum += item.Value * item.Key.Weight;
                }
                return sum;
            }
        }

        public Buy(params Product[] products)
            : base(products)
        {
        }

        public override string ToString() =>
            $"{base.ToString()}\nTotal Weight: {Weight}\nTotal Price: {Price}";
    }
}

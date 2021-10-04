using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    class ProductCollection : IEnumerable<KeyValuePair<Product, uint>>
    {
        public ProductCollection(params Product[] products)
        {
            _data = new Dictionary<Product, uint>();
            Add(products);
        }

        public ICollection<Product> Products => _data.Keys;

        public static ProductCollection Parse(string s)
        {
            string[] prs = s.Split('\n');
            var result = new ProductCollection();
            foreach (string pr in prs)
                result.Add(Product.Parse(pr));
            return result;
        }

        public void Add(Product pr, uint count = 1)
        {
            if (count == 0) return;
            if (_data.ContainsKey(pr)) _data[pr] += count;
            else _data[pr] = count;
        }

        public void Add(IEnumerable<Product> products)
        {
            foreach (Product item in products) Add(item);
        }

        public void Remove(Product pr)
        {
            if (_data.ContainsKey(pr))
            {
                if (_data[pr] > 1) _data[pr]--;
                else _data.Remove(pr);
            }
        }

        public void RemoveAll(Product pr) => _data.Remove(pr);

        public override string ToString()
        {
            var res = new StringBuilder();
            foreach (var item in _data)
            {
                res.AppendLine($"[{item.Value}] {item.Key}");
            }
            return res.ToString();
        }

        public IEnumerator<KeyValuePair<Product, uint>> GetEnumerator() =>
            _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            throw new System.NotImplementedException();

        Dictionary<Product, uint> _data;
    }
}

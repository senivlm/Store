using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Products : IEnumerable<KeyValuePair<Product, uint>>
    {
        Dictionary<Product, uint> _data;

        public Products(IEnumerable<Product> products)
        {
            _data = new();
            Add(products);
        }

        public Products(params Product[] products)
            : this(products as IEnumerable<Product>)
        { }

        public ICollection<Product> UniqueProducts => _data.Keys;

        public static Products Parse(string[] entries)
        {
            Products result = new();
            foreach (string pr in entries)
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

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}

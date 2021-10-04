using System;

namespace Store
{
    class Program
    {
        static void Main()
        {
            //st.GetProductFromConsole();
            var storage = new Storage();
            storage.GetProductsFromFile(@"C:\Users\ihorm\source\repos\Store\products.txt");
            Console.WriteLine(storage);

            Console.Read();
        }
    }
}

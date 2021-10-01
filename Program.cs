using System;

namespace Store
{
    class Program
    {
        static void Main()
        {
            //Buy list = new Buy(
            //    new Product("Milk", 3.5, 1),
            //    new Product("Milk", 3.5, 1),
            //    new Product("Milk", 3.5, 1),
            //    new Product("Bread", 1.5, .3),
            //    new Product("Soda", 1, .5),
            //    new Product(),
            //    new Product()
            //);

            //Check.PrintBuyIntoConsole(list);

            Storage st = new Storage(
                new Dairy("Milk", 3.5, 1, 10),
                new Meat("Sausage", 2, .12, QualityGrade.Highest, MeatType.Pork),
                new Product("Bread", 1.5, .3),
                new Product("Soda", 1, .5)
            );

            st.GetProductFromConsole();

            Console.WriteLine(st);

            Console.Read();
        }
    }
}

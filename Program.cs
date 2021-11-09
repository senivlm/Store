﻿using System;

namespace Store
{
    class Program
    {
        static void Main()
        {
            var storage = new Storage();
            // Чому одразу не записуєтесь на прослуховування події.
            storage.GetProductsFromFile(@"C:\Users\ihorm\source\repos\Store\products.txt");

            //try again version
            storage.OnGetFromConsoleFail += (obj, args) =>
            {
                (obj as Storage).GetProductFromConsole();
            };

            //log version
            storage.OnGetFromConsoleFail += (obj, args) =>
            {
                System.IO.File.AppendAllLines(
                    @"C:\Users\ihorm\source\repos\Store\log.txt",
                    new[]
                    {
                        $"{DateTime.Now}",
                        $"Args log:",
                        $"name: [{args.Name}]",
                        $"price: [{args.Price}]",
                        $"weight: [{args.Weight}]",
                        $"consumeIn: [{args.ConsumeIn}]",
                        $"productType: [{args.TypeOfProduct}]",
                        $"qualityGrade: [{args.Quality}]",
                        $"meatType: [{args.MeatType}]",
                    }
                    );
            };

            storage.OnDisplay += (obj, args) =>
            {
                (obj as Storage).RemoveSpoiledAndRecordToFile(@"C:\Users\ihorm\source\repos\Store\log.txt");
            };

            storage.GetProductFromConsole();

            Console.WriteLine(storage);
            Console.Read();
        }
    }
}

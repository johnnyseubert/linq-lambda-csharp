using System;
using System.Linq;
using System.Collections.Generic;
using LinqWithLambda.Entities;

namespace LinqWithLambda {
    class Program {

        static void Print<T> (string message, IEnumerable<T> collection) {
            Console.WriteLine(message);

            foreach ( var item in collection ) {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void Main (string[] args) {

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 3 };

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0 , Category = c2},
                new Product() { Id = 2, Name = "Hammer", Price = 90.0 , Category = c1},
                new Product() { Id = 3, Name = "Tv", Price = 1700.0 , Category = c3},
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0 , Category = c2},
                new Product() { Id = 5, Name = "Saw", Price = 80.0 , Category = c1},
                new Product() { Id = 6, Name = "Tablet", Price = 700.0 , Category = c2},
                new Product() { Id = 7, Name = "Camera", Price = 700.0 , Category = c3},
                new Product() { Id = 8, Name = "Printer", Price = 350.0 , Category = c3},
                new Product() { Id = 9, Name = "Macbook", Price = 1800.0 , Category = c2},
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0 , Category = c3},
                new Product() { Id = 11, Name = "Level", Price = 70.0 , Category = c1},
            };

            Console.WriteLine("Products that are from Tier 1 and Price lower then $900.0");

            //var r1 = products.Where(x => x.Category.Tier == 1 && x.Price < 900.0);
            var r1 = from p in products where p.Category.Tier == 1 && p.Price < 900.0 select p;
            Print("Tier 1 and price < 900:", r1);

            
            //var r2 = products.Where(prod => prod.Category.Name == "Tools").Select(prod => prod.Name);
            var r2 = from p in products where p.Category.Name == "Tools" select p.Name;
            Print("Category name = Tools", r2);

            
            //var r3 = products.Where(prod => prod.Name[0] == 'C').Select(prod => new { prod.Name, prod.Price, CategoryName = prod.Category.Name });
            var r3 = from p in products where p.Name[0] == 'C' select new {p.Name, p.Price, CategoryName = p.Category.Name};
            Print("Names started with 'C':", r3);

            
            //var r4 = products.Where(prod => prod.Category.Tier == 1).OrderBy(prod => prod.Price).ThenBy(prod => prod.Name);
            var r4 = from p in products where p.Category.Tier == 1 orderby p.Name orderby p.Price select p.Name;
            Print("Tier 1 ordered by price then by name", r4);

            
            //var r5 = r4.Skip(2).Take(4);
            var r5 = (from p in r4 select p).Skip(2).Take(4);
            Print("Tier 1 ordered by price then by name skip 2 take 4", r5);

            
            var r6 = products.Where(prod => prod.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or Default: "+ r6);

            
            var r10 = products.Max(p => p.Price);
            Console.WriteLine("Max Price: " + r10);

            
            var r11 = products.Min(p => p.Price);
            Console.WriteLine("Min price: " + r11);

            
            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Sum prices: " + r12);

            
            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Average prices: " + r13);

            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average prices 5: " + r14);

            var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate((x,y) => x + y);
            Console.WriteLine("Category 1 Aggregate sum: " +r15);

            var r16 = products.Where(p => p.Category.Id == 6).Select(p => p.Price).Aggregate(0.0,(x,y) => x + y);
            Console.WriteLine("Category 1 Aggregate sum: " +r16);

            var r17 = products.GroupBy(p => p.Category);

            foreach (var item in r17)
            {
                Console.WriteLine("Category " + item.Key.Name + ":");
                foreach (var p in item)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
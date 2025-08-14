using System;
using System.Collections.Generic;
using System.Linq;

public record Product(string Name, decimal Price, int Quantity);

public class Inventory
{
    private List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }

    public decimal CalculateTotalValue()
    {
        return products.Sum(p => p.Price * p.Quantity);
    }

    public void DisplayProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var inventory = new Inventory();

        var product1 = new Product("Apple", 1.99m, 10);
        var product2 = new Product("Banana", 0.99m, 20);
        var product3 = new Product("Orange", 2.49m, 15);

        inventory.AddProduct(product1);
        inventory.AddProduct(product2);
        inventory.AddProduct(product3);

        Console.WriteLine("Total Value: " + inventory.CalculateTotalValue());

        inventory.DisplayProducts();
    }
}

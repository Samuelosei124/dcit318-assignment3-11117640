using System;

class Program
{
    static void Main(string[] args)
    {
        var manager = new WareHouseManager();
        manager.SeedData();

        Console.WriteLine("\n--- Grocery Items ---");
        manager.PrintAllItems(manager.Groceries);
        Console.WriteLine("\n--- Electronic Items ---");
        manager.PrintAllItems(manager.Electronics);

        // Try to add a duplicate item
        Console.WriteLine("\n--- Adding Duplicate Electronic Item ---");
        try
        {
            manager.Electronics.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12));
        }
        catch (DuplicateItemException ex)
        {
            Console.WriteLine($"DuplicateItemException: {ex.Message}");
        }

        // Try to remove a non-existent item
        Console.WriteLine("\n--- Removing Non-Existent Grocery Item ---");
        try
        {
            manager.RemoveItemById(manager.Groceries, 999);
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"ItemNotFoundException: {ex.Message}");
        }

        // Try to update with invalid quantity
        Console.WriteLine("\n--- Updating with Invalid Quantity ---");
        try
        {
            manager.Electronics.UpdateQuantity(2, -10);
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"InvalidQuantityException: {ex.Message}");
        }

        Console.WriteLine("\n--- Program End ---");
    }
}

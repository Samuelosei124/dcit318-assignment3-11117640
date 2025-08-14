using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventorySystem
{
    // Marker interface for logging
    public interface IInventoryEntity
    {
        int Id { get; }
    }

    // Immutable Inventory Record
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

    // Generic Inventory Logger
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_log);
        }

        public void SaveToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                using (var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.Serialize(stream, _log, options);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("File does not exist. No data loaded.");
                    return;
                }
                using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    var loaded = JsonSerializer.Deserialize<List<T>>(stream);
                    _log = loaded ?? new List<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        public void Clear()
        {
            _log.Clear();
        }
    }

    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;
        private const string FilePath = "inventory.json";

        public InventoryApp()
        {
            _logger = new InventoryLogger<InventoryItem>(FilePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Laptop", 5, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Monitor", 10, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Keyboard", 20, DateTime.Now));
            _logger.Add(new InventoryItem(4, "Mouse", 25, DateTime.Now));
            _logger.Add(new InventoryItem(5, "Printer", 3, DateTime.Now));
        }

        public void SaveData()
        {
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            var items = _logger.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("No inventory items found.");
                return;
            }
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
            }
        }

        public void ClearMemory()
        {
            _logger.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var app = new InventoryApp();
            Console.WriteLine("Seeding sample data...");
            app.SeedSampleData();
            Console.WriteLine("Saving data to file...");
            app.SaveData();

            // Clear memory to simulate new session
            app.ClearMemory();
            Console.WriteLine("Memory cleared. Loading data from file...");
            app.LoadData();
            Console.WriteLine("Printing all inventory items:");
            app.PrintAllItems();
        }
    }
}

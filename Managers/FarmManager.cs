public class FarmManager
{
    private string farmName;
    private List<Animal> animals;
    private List<Crop> crops;
    private List<Product> inventory;
    private decimal totalProfit;
    private int currentDay;

    public FarmManager(string farmName)
    {
        this.farmName = farmName;
        this.animals = new List<Animal>();
        this.crops = new List<Crop>();
        this.inventory = new List<Product>();
        this.totalProfit = 0;
        this.currentDay = 1;
    }

    public string FarmName => farmName;
    public decimal TotalProfit => totalProfit;
    public int CurrentDay => currentDay;

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
        Console.WriteLine($"Added {animal.GetType().Name} '{animal.Name}' to the farm.");
    }

    public void AddCrop(Crop crop)
    {
        crops.Add(crop);
        Console.WriteLine($"Planted {crop.GetType().Name} '{crop.Name}' on the farm.");
    }

    public void FeedAllAnimals(int foodAmount)
    {
        Console.WriteLine("\n--- Feeding All Animals ---");
        foreach (var animal in animals)
        {
            try
            {
                animal.Feed(foodAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error feeding {animal.Name}: {ex.Message}");
            }
        }
    }

    public void FeedAnimal(string name, int amount)
    {
        var animal = animals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (animal == null)
        {
            Console.WriteLine($"Animal '{name}' not found.");
            return;
        }
        
        try
        {
            animal.Feed(amount);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ProduceFromAllAnimals()
    {
        Console.WriteLine("\n--- Producing from All Animals ---");
        foreach (var animal in animals)
        {
            try
            {
                Product product = animal.Produce();
                AddToInventory(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error producing from {animal.Name}: {ex.Message}");
            }
        }
    }

    public void GrowAllCrops(int days)
    {
        Console.WriteLine($"\n--- Growing All Crops ({days} days) ---");
        foreach (var crop in crops.Where(c => !c.IsHarvested))
        {
            try
            {
                crop.Grow(days);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error growing {crop.Name}: {ex.Message}");
            }
        }
    }

    public void HarvestCrop(string name)
    {
        var crop = crops.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (crop == null)
        {
            Console.WriteLine($"Crop '{name}' not found.");
            return;
        }
        
        try
        {
            Product product = crop.Harvest();
            AddToInventory(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void HarvestAllMatureCrops()
    {
        Console.WriteLine("\n--- Harvesting All Mature Crops ---");
        foreach (var crop in crops.Where(c => c.IsMature && !c.IsHarvested).ToList())
        {
            try
            {
                Product product = crop.Harvest();
                AddToInventory(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error harvesting {crop.Name}: {ex.Message}");
            }
        }
    }

    private void AddToInventory(Product product)
    {
        var existing = inventory.FirstOrDefault(p => p.GetType() == product.GetType());
        if (existing != null)
        {
            existing.SetQuantity(existing.Quantity + product.Quantity);
        }
        else
        {
            inventory.Add(product);
        }
        Console.WriteLine($"Added {product.Quantity} units of {product.Name} to inventory.");
    }

    public void SellProduct(string productName, int quantity)
    {
        var product = inventory.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        if (product == null)
        {
            Console.WriteLine($"Product '{productName}' not found in inventory.");
            return;
        }
        
        try
        {
            decimal profit = product.Sell(quantity);
            totalProfit += profit;
            Console.WriteLine($"Sold {quantity} units of {productName} for K{profit:F2}. Total profit: K{totalProfit:F2}");
            
            if (product.Quantity == 0)
                inventory.Remove(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void SimulateDay()
    {
        Console.WriteLine($"\n========== DAY {currentDay} SIMULATION ==========");
        
        // Animals lose food over time
        foreach (var animal in animals)
        {
            animal.DecreaseFood(10);
        }
        
        // Sheep grow wool
        foreach (var sheep in animals.OfType<Sheep>())
        {
            sheep.GrowWool(5);
        }
        
        currentDay++;
        Console.WriteLine($"Day {currentDay - 1} completed. New day begins.\n");
    }

    public void DisplayReport()
    {
        Console.WriteLine($"\n{'=',60}");
        Console.WriteLine($"FARM REPORT - {farmName.ToUpper()}");
        Console.WriteLine($"{'=',60}");
        Console.WriteLine($"Current Day: {currentDay}");
        Console.WriteLine($"Total Profit: K{totalProfit:F2}");
        
        Console.WriteLine($"\n--- ANIMALS ({animals.Count}) ---");
        if (animals.Count == 0)
            Console.WriteLine("No animals on the farm.");
        foreach (var animal in animals)
        {
            Console.WriteLine($"  {animal.GetInfo()}");
        }
        
        Console.WriteLine($"\n--- CROPS ({crops.Count}) ---");
        if (crops.Count == 0)
            Console.WriteLine("No crops planted.");
        foreach (var crop in crops)
        {
            Console.WriteLine($"  {crop.GetInfo()}");
        }
        
        Console.WriteLine($"\n--- INVENTORY ({inventory.Count} product types) ---");
        if (inventory.Count == 0)
            Console.WriteLine("Inventory is empty.");
        foreach (var product in inventory)
        {
            Console.WriteLine($"  {product}");
        }
        
        Console.WriteLine($"\n--- RECENT ACTIONS ---");
        var allEntities = animals.Cast<FarmEntity>().Concat(crops.Cast<FarmEntity>()).ToList();
        var recentActions = allEntities
            .SelectMany(e => e.GetActionHistory().Select(a => new { Entity = e.Name, Action = a }))
            .OrderByDescending(x => x.Action.Date)
            .Take(15);
        
        if (!recentActions.Any())
            Console.WriteLine("No actions recorded yet.");
        foreach (var item in recentActions)
        {
            Console.WriteLine($"  {item.Entity}: {item.Action}");
        }
        
        Console.WriteLine($"{'=',60}\n");
    }
}
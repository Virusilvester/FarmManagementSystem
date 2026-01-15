public class FarmManager
{
    private Dictionary<string, FarmEntity> _entities;
    private decimal _totalProfit;
    private int _day;

    public FarmManager()
    {
        _entities = new Dictionary<string, FarmEntity>();
        _totalProfit = 0m;
        _day = 1;
    }

    public void AddEntity(FarmEntity entity)
    {
        _entities[entity.Id] = entity;
        Console.WriteLine($"Added {entity.GetType().Name}: {entity.Name} (ID: {entity.Id})");
    }

    public void FeedAnimal(string id, int amount)
    {
        if (!_entities.ContainsKey(id))
            throw new KeyNotFoundException("Animal not found");
            
        var animal = _entities[id] as Animal;
        if (animal == null)
            throw new InvalidOperationException("Entity is not an animal");
            
        animal.Feed(amount);
    }

    public void GrowCrop(string id)
    {
        if (!_entities.ContainsKey(id))
            throw new KeyNotFoundException("Crop not found");
            
        var crop = _entities[id] as Crop;
        if (crop == null)
            throw new InvalidOperationException("Entity is not a crop");
            
        crop.Grow();
    }

    public string HarvestCrop(string id)
    {
        if (!_entities.ContainsKey(id))
            throw new KeyNotFoundException("Crop not found");
            
        var crop = _entities[id] as Crop;
        if (crop == null)
            throw new InvalidOperationException("Entity is not a crop");
            
        string product = crop.Harvest();
        Console.WriteLine($"Harvested {product} from {crop.Name}");
        return product;
    }

    public decimal SellProduct(string cropId, int quantity)
    {
        if (!_entities.ContainsKey(cropId))
            throw new KeyNotFoundException("Crop not found");
            
        var sellable = _entities[cropId] as Sellable;
        if (sellable == null)
            throw new InvalidOperationException("Entity cannot be sold");
            
        decimal profit = sellable.Sell(quantity);
        _totalProfit += profit;
        Console.WriteLine($"Sold {quantity} units for {profit:C}. Total profit: {_totalProfit:C}");
        return profit;
    }

    public void SimulateDay()
    {
        Console.WriteLine($"\n========== DAY {_day++} SIMULATION ==========");
        
        // Auto-grow crops
        foreach (var entity in _entities.Values)
        {
            if (entity is Crop crop)
            {
                crop.NewDay();
                Console.WriteLine($"{crop.Name} grew. {crop.GetStatus()}");
            }
        }

        // Auto-feed animals (basic simulation)
        foreach (var entity in _entities.Values)
        {
            if (entity is Animal animal && animal.IsHungry)
            {
                try
                {
                    animal.Feed(10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not feed {animal.Name}: {ex.Message}");
                }
            }
        }
    }

    public void DisplayReport()
    {
        Console.WriteLine("\n========== FARM REPORT ==========");
        Console.WriteLine($"Total Profit: {_totalProfit:C}");
        Console.WriteLine($"Day: {_day}");
        Console.WriteLine($"\nAnimals:");
        
        foreach (var entity in _entities.Values.Where(e => e is Animal))
        {
            Console.WriteLine($"  - {entity.GetStatus()}");
            Console.WriteLine($"    Sound: {((Animal)entity).MakeSound()}");
            Console.WriteLine($"    Recent Actions:");
            foreach (var action in entity.ActionHistory.TakeLast(3))
            {
                Console.WriteLine($"      * {action}");
            }
        }

        Console.WriteLine($"\nCrops:");
        foreach (var entity in _entities.Values.Where(e => e is Crop))
        {
            Console.WriteLine($"  - {entity.GetStatus()}");
            Console.WriteLine($"    Recent Actions:");
            foreach (var action in entity.ActionHistory.TakeLast(3))
            {
                Console.WriteLine($"      * {action}");
            }
        }
    }
}
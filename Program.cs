class Program
{
    static void Main()
    {
        FarmManager manager = new FarmManager();
        
        while (true)
        {
            Console.WriteLine("\n=== FARM MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Add Animal");
            Console.WriteLine("2. Add Crop");
            Console.WriteLine("3. Feed Animal");
            Console.WriteLine("4. Grow Crop");
            Console.WriteLine("5. Harvest Crop");
            Console.WriteLine("6. Sell Products");
            Console.WriteLine("7. Simulate Next Day");
            Console.WriteLine("8. Display Report");
            Console.WriteLine("9. Exit");
            Console.Write("Select: ");
            
            switch (Console.ReadLine())
            {
                case "1":
                    AddAnimalMenu(manager);
                    break;
                case "2":
                    AddCropMenu(manager);
                    break;
                case "3":
                    FeedAnimalMenu(manager);
                    break;
                case "4":
                    GrowCropMenu(manager);
                    break;
                case "5":
                    HarvestCropMenu(manager);
                    break;
                case "6":
                    SellProductMenu(manager);
                    break;
                case "7":
                    manager.SimulateDay();
                    break;
                case "8":
                    manager.DisplayReport();
                    break;
                case "9":
                    return;
                default:
                    Console.WriteLine("Invalid selection!");
                    break;
            }
        }
    }

    static void AddAnimalMenu(FarmManager manager)
    {
        Console.Write("Enter animal name: ");
        string name = Console.ReadLine();
        Console.Write("Select type (1=Cow, 2=Chicken, 3=Sheep): ");
        
        switch (Console.ReadLine())
        {
            case "1":
#pragma warning disable CS8604 // Possible null reference argument.
                manager.AddEntity(new Cow(name));
#pragma warning restore CS8604 // Possible null reference argument.
                break;
            case "2":
                manager.AddEntity(new Chicken(name));
                break;
            case "3":
                manager.AddEntity(new Sheep(name));
                break;
            default:
                Console.WriteLine("Invalid type!");
                break;
        }
    }

    static void AddCropMenu(FarmManager manager)
    {
        Console.Write("Enter crop name: ");
        string name = Console.ReadLine();
        Console.Write("Select type (1=Wheat, 2=Corn, 3=Vegetables): ");
        
        switch (Console.ReadLine())
        {
            case "1":
                manager.AddEntity(new Wheat(name));
                break;
            case "2":
                manager.AddEntity(new Corn(name));
                break;
            case "3":
                manager.AddEntity(new Vegetables(name));
                break;
            default:
                Console.WriteLine("Invalid type!");
                break;
        }
    }

    static void FeedAnimalMenu(FarmManager manager)
    {
        Console.Write("Enter animal ID: ");
        string id = Console.ReadLine();
        Console.Write("Enter food amount: ");
        
        if (int.TryParse(Console.ReadLine(), out int amount))
        {
            try
            {
                manager.FeedAnimal(id, amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void GrowCropMenu(FarmManager manager)
    {
        Console.Write("Enter crop ID: ");
        string id = Console.ReadLine();
        
        try
        {
            manager.GrowCrop(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void HarvestCropMenu(FarmManager manager)
    {
        Console.Write("Enter crop ID: ");
        string id = Console.ReadLine();
        
        try
        {
            manager.HarvestCrop(id);
        }
        catch (CropNotMatureException ex)
        {
            Console.WriteLine($"Not ready: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SellProductMenu(FarmManager manager)
    {
        Console.Write("Enter crop/animal product ID: ");
        string id = Console.ReadLine();
        Console.Write("Enter quantity: ");
        
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            try
            {
                manager.SellProduct(id, quantity);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid quantity: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
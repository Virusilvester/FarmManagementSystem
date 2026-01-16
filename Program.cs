class Program
{
    static void Main()
    {
        FarmManager farm = new FarmManager("BroadLay Farm");
        bool running = true;

        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║   FARM MANAGEMENT SYSTEM  V2.0             ║");
        Console.WriteLine("╚════════════════════════════════════════════╝\n");

        while (running)
        {
            Console.WriteLine("\n--- MAIN MENU ---");
            Console.WriteLine("1.  Add Animal");
            Console.WriteLine("2.  Add Crop");
            Console.WriteLine("3.  Feed Animal");
            Console.WriteLine("4.  Feed All Animals");
            Console.WriteLine("5.  Produce from All Animals");
            Console.WriteLine("6.  Grow All Crops");
            Console.WriteLine("7.  Harvest Crop");
            Console.WriteLine("8.  Harvest All Mature Crops");
            Console.WriteLine("9.  Sell Product");
            Console.WriteLine("10. Simulate Day");
            Console.WriteLine("11. Display Farm Report");
            Console.WriteLine("12. Exit");
            Console.Write("\nEnter choice: ");

#pragma warning disable CS8600, CS8604
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddAnimalMenu(farm);
                        break;
                    case "2":
                        AddCropMenu(farm);
                        break;
                    case "3":
                        FeedAnimalMenu(farm);
                        break;
                    case "4":
                        Console.Write("Enter food amount for each animal: ");
                        int foodAmount = int.Parse(Console.ReadLine());
                        farm.FeedAllAnimals(foodAmount);
                        break;
                    case "5":
                        farm.ProduceFromAllAnimals();
                        break;
                    case "6":
                        Console.Write("Enter number of days to grow: ");
                        int days = int.Parse(Console.ReadLine());
                        farm.GrowAllCrops(days);
                        break;
                    case "7":
                        HarvestCropMenu(farm);
                        break;
                    case "8":
                        farm.HarvestAllMatureCrops();
                        break;
                    case "9":
                        SellProductMenu(farm);
                        break;
                    case "10":
                        farm.SimulateDay();
                        break;
                    case "11":
                        farm.DisplayReport();
                        break;
                    case "12":
                        running = false;
                        Console.WriteLine("\nThank you for using Farm Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input format. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void AddAnimalMenu(FarmManager farm)
    {
        Console.WriteLine("\nSelect Animal Type:");
        Console.WriteLine("1. Cow");
        Console.WriteLine("2. Chicken");
        Console.WriteLine("3. Sheep");
        Console.Write("Enter choice: ");
        string type = Console.ReadLine();

        Console.Write("Enter animal name: ");
        string name = Console.ReadLine();

        Animal animal = type switch
        {
            "1" => new Cow(name),
            "2" => new Chicken(name),
            "3" => new Sheep(name),
            _ => null
        };

        if (animal != null)
        {
            farm.AddAnimal(animal);
            Console.WriteLine($"{animal.MakeSound()}");
        }
        else
        {
            Console.WriteLine("Invalid animal type.");
        }
    }

    static void AddCropMenu(FarmManager farm)
    {
        Console.WriteLine("\nSelect Crop Type:");
        Console.WriteLine("1. Wheat (7 days to mature)");
        Console.WriteLine("2. Corn (10 days to mature)");
        Console.WriteLine("3. Vegetables (5 days to mature)");
        Console.Write("Enter choice: ");
        string type = Console.ReadLine();

        Console.Write("Enter crop name: ");
        string name = Console.ReadLine();

        Crop crop = type switch
        {
            "1" => new Wheat(name),
            "2" => new Corn(name),
            "3" => new VegetableCrop(name),
            _ => null
        };

        if (crop != null)
        {
            farm.AddCrop(crop);
        }
        else
        {
            Console.WriteLine("Invalid crop type.");
        }
    }

    static void FeedAnimalMenu(FarmManager farm)
    {
        Console.Write("Enter animal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter food amount: ");
        int amount = int.Parse(Console.ReadLine());
        farm.FeedAnimal(name, amount);
    }

    static void HarvestCropMenu(FarmManager farm)
    {
        Console.Write("Enter crop name to harvest: ");
        string name = Console.ReadLine();
        farm.HarvestCrop(name);
    }

    static void SellProductMenu(FarmManager farm)
    {
        Console.Write("Enter product name (Milk/Eggs/Wool/Grain/Corn Cobs/Vegetables): ");
        string productName = Console.ReadLine();
        Console.Write("Enter quantity to sell: ");
        int quantity = int.Parse(Console.ReadLine());
        farm.SellProduct(productName, quantity);
    }
}
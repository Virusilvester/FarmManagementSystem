public class Chicken : Animal
{
    public Chicken(string name) : base(name) { }

    public override Product Produce()
    {
        if (Health < 30)
            throw new InvalidOperationException($"{Name} is too unhealthy to lay eggs");
        if (FoodLevel < 15)
            throw new InsufficientFoodException($"{Name} needs more food to lay eggs");
        
        int eggAmount = Math.Max(1, Health / 20);
        AddAction("Produce", eggAmount);
        Console.WriteLine($"{Name} laid {eggAmount} eggs. {MakeSound()}");
        return new Eggs(eggAmount);
    }

    public override string MakeSound()
    {
        return "Cluck cluck!";
    }
}
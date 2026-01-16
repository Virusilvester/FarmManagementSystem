public class Cow : Animal
{
    public Cow(string name) : base(name) { }

    public override Product Produce()
    {
        if (Health < 30)
            throw new InvalidOperationException($"{Name} is too unhealthy to produce milk");
        if (FoodLevel < 20)
            throw new InsufficientFoodException($"{Name} needs more food to produce milk");
        
        int milkAmount = Health / 10;
        AddAction("Produce", milkAmount);
        Console.WriteLine($"{Name} produced {milkAmount} units of milk. {MakeSound()}");
        return new Milk(milkAmount);
    }

    public override string MakeSound()
    {
        return "Moooo!";
    }
}
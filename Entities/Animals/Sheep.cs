public class Sheep : Animal
{
    private int woolGrowth;

    public Sheep(string name) : base(name)
    {
        woolGrowth = 50;
    }

    public int WoolGrowth => woolGrowth;

    public void GrowWool(int amount)
    {
        woolGrowth = Math.Min(100, woolGrowth + amount);
    }

    public override Product Produce()
    {
        if (woolGrowth < 50)
            throw new InvalidOperationException($"{Name}'s wool hasn't grown enough yet (needs 50, has {woolGrowth})");
        if (Health < 30)
            throw new InvalidOperationException($"{Name} is too unhealthy to be sheared");
        
        int woolAmount = woolGrowth / 10;
        woolGrowth = 0;
        AddAction("Produce", woolAmount);
        Console.WriteLine($"{Name} was sheared for {woolAmount} units of wool. {MakeSound()}");
        return new Wool(woolAmount);
    }

    public override string MakeSound()
    {
        return "Baaaa!";
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $", Wool Growth: {woolGrowth}";
    }
}
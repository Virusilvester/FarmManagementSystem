public class Cow : Animal
{
    public Cow(string name) : base(name) { }

    public override void Feed(int amount)
    {
        ValidateFeedAmount(amount);
        UpdateHunger(amount);
        AddAction("Feed", amount);
        Console.WriteLine($"Cow {Name} fed {amount} units. Moo!");
    }

    public override string MakeSound() => "Moooo!";

    public override string Produce()
    {
        if (IsHungry)
            throw new Exception("Cow is too hungry to produce milk");
        
        AddAction("Produce", 1);
        return "Milk";
    }
}
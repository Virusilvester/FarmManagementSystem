public class Chicken : Animal
{
    public Chicken(string name) : base(name) { }

    public override void Feed(int amount)
    {
        ValidateFeedAmount(amount);
        UpdateHunger(amount);
        AddAction("Feed", amount);
        Console.WriteLine($"Chicken {Name} fed {amount} units. Cluck!");
    }

    public override string MakeSound() => "Cluck cluck!";

    public override string Produce()
    {
        if (IsHungry)
            throw new Exception("Chicken is too hungry to lay eggs");
            
        AddAction("Produce", 3); // 3 eggs
        return "Eggs";
    }
}
public class Sheep : Animal
{
    private bool _isSheared = false;

    public Sheep(string name) : base(name) { }

    public override void Feed(int amount)
    {
        ValidateFeedAmount(amount);
        UpdateHunger(amount);
        AddAction("Feed", amount);
        Console.WriteLine($"Sheep {Name} fed {amount} units. Baa!");
    }

    public override string MakeSound() => "Baaaa!";

    public override string Produce()
    {
        if (IsHungry)
            throw new Exception("Sheep is too hungry to produce wool");
            
        if (_isSheared)
            throw new Exception("Sheep was already sheared this season");

        _isSheared = true;
        AddAction("Produce", 1);
        return "Wool";
    }
}
public class Wheat : Crop, Sellable
{
    public Wheat(string name) : base(name, 7) { }

    public override void Grow()
    {
        Water(10);
        AddAction("Grow", 1);
        Console.WriteLine($"Wheat {Name} watered and growing. Rustle rustle.");
    }

    public override string Harvest()
    {
        ValidateGrowth();
        AddAction("Harvest", 1);
        _growthDays = 0; // Reset after harvest
        return "Grain";
    }

    public override string Produce()
    {
        return Harvest();
    }

    public decimal Sell(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidQuantityException("Quantity must be > 0");
            
        AddAction("Sell", quantity);
        return quantity * 2.5m; // $2.50 per unit
    }
}
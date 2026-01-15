public class Vegetables : Crop, Sellable
{
    public Vegetables(string name) : base(name, 5) { }

    public override void Grow()
    {
        Water(8);
        AddAction("Grow", 1);
        Console.WriteLine($"Vegetables {Name} watered and growing. Rustle rustle.");
    }

    public override string Harvest()
    {
        ValidateGrowth();
        AddAction("Harvest", 1);
        _growthDays = 0;
        return "Vegetables";
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
        return quantity * 3.0m; // $3.00 per unit
    }
}
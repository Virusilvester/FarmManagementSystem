public class Corn : Crop, Sellable
{
    public Corn(string name) : base(name, 10) { }

    public override void Grow()
    {
        Water(15);
        AddAction("Grow", 1);
        Console.WriteLine($"Corn {Name} watered and growing. Rustle rustle.");
    }

    public override string Harvest()
    {
        ValidateGrowth();
        AddAction("Harvest", 1);
        _growthDays = 0;
        return "CornCobs";
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
        return quantity * 1.8m; // $1.80 per unit
    }
}
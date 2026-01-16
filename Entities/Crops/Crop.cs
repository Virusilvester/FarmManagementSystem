public abstract class Crop : FarmEntity
{
    private int growthStage;
    private int maturityLevel;
    private bool isHarvested;

    protected Crop(string name, int maturityLevel) : base(name)
    {
        this.growthStage = 0;
        this.maturityLevel = maturityLevel;
        this.isHarvested = false;
    }

    public int GrowthStage => growthStage;
    public int MaturityLevel => maturityLevel;
    public bool IsHarvested => isHarvested;
    public bool IsMature => growthStage >= maturityLevel;

    public void Grow(int days)
    {
        if (isHarvested)
            throw new InvalidOperationException($"{Name} has already been harvested");
        if (days <= 0)
            throw new InvalidQuantityException("Growth days must be positive");
        
        growthStage += days;
        AddAction("Grow", days);
        Console.WriteLine($"{Name} grew {days} days. Growth: {growthStage}/{maturityLevel}");
    }

    public Product Harvest()
    {
        if (isHarvested)
            throw new CropNotMatureException($"{Name} has already been harvested");
        if (!IsMature)
            throw new CropNotMatureException($"{Name} is not mature yet. Growth: {growthStage}/{maturityLevel}");
        
        isHarvested = true;
        Product product = Produce();
        AddAction("Harvest", product.Quantity);
        Console.WriteLine($"{Name} harvested! Produced {product.Quantity} units of {product.Name}");
        return product;
    }

    public override string GetInfo()
    {
        string status = isHarvested ? "Harvested" : (IsMature ? "Ready to Harvest" : "Growing");
        return $"{GetType().Name} '{Name}' [ID: {Id}] - Growth: {growthStage}/{maturityLevel}, Status: {status}";
    }
}
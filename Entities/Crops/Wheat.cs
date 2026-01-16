public class Wheat : Crop
{
    public Wheat(string name) : base(name, 7) { }

    public override Product Produce()
    {
        int grainAmount = GrowthStage * 5;
        return new Grain(grainAmount);
    }
}
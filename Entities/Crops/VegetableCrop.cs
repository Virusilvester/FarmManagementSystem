public class VegetableCrop : Crop
{
    public VegetableCrop(string name) : base(name, 5) { }

    public override Product Produce()
    {
        int vegAmount = GrowthStage * 4;
        return new Vegetables(vegAmount);
    }
}
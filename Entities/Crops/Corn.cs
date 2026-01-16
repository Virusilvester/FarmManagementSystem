public class Corn : Crop
{
    public Corn(string name) : base(name, 10) { }

    public override Product Produce()
    {
        int cornAmount = GrowthStage * 3;
        return new CornCobs(cornAmount);
    }
}
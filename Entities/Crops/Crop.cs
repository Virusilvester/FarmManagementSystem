public abstract class Crop : FarmEntity
{
    protected int _growthDays;
    private int _daysToMaturity;
    private bool _isWateredToday;

    protected Crop(string name, int daysToMaturity) : base(name)
    {
        _growthDays = 0;
        _daysToMaturity = daysToMaturity;
        _isWateredToday = false;
    }

    public abstract void Grow();
    public abstract string Harvest();

    protected void ValidateGrowth()
    {
        if (_growthDays < _daysToMaturity)
            throw new CropNotMatureException($"Crop needs {_daysToMaturity - _growthDays} more days to mature");
    }

    protected void Water(int amount)
    {
        if (amount <= 0)
            throw new InvalidQuantityException("Water amount must be > 0");
            
        _isWateredToday = true;
    }

    public override string GetStatus()
    {
        return $"Crop {Name} (ID: {Id}) - Growth: {_growthDays}/{_daysToMaturity} days, Watered: {_isWateredToday}";
    }

    // Simulate a day passing
    public void NewDay()
    {
        if (_isWateredToday)
        {
            _growthDays++;
        }
        _isWateredToday = false;
    }
}
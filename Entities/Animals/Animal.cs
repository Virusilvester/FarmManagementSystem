public abstract class Animal : FarmEntity
{
    private int _hungerLevel;
    private int _foodConsumedToday;

    protected Animal(string name, int hungerLevel = 50) : base(name)
    {
        _hungerLevel = hungerLevel;
        _foodConsumedToday = 0;
    }

    // Abstract methods
    public abstract void Feed(int amount);
    public abstract string MakeSound();
    
    // Common animal logic
    public bool IsHungry => _hungerLevel > 30;

    protected void ValidateFeedAmount(int amount)
    {
        if (amount <= 0)
            throw new InsufficientFoodException("Feed amount must be > 0");
    }

    protected void UpdateHunger(int amount)
    {
        _foodConsumedToday = amount;
        _hungerLevel = Math.Max(0, _hungerLevel - amount);
    }

    public override string GetStatus()
    {
        return $"Animal {Name} (ID: {Id}) - Hunger: {_hungerLevel}, Fed: {_foodConsumedToday} units today";
    }
}
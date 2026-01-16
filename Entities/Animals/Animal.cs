public abstract class Animal : FarmEntity
{
    private int foodLevel;
    private int health;
    private const int MAX_FOOD = 100;
    private const int MAX_HEALTH = 100;

    protected Animal(string name) : base(name)
    {
        this.foodLevel = 50;
        this.health = 100;
    }

    public int FoodLevel => foodLevel;
    public int Health => health;

    public void Feed(int amount)
    {
        if (amount <= 0)
            throw new InsufficientFoodException("Feed amount must be greater than 0");
        
        foodLevel = Math.Min(MAX_FOOD, foodLevel + amount);
        health = Math.Min(MAX_HEALTH, health + (amount / 2));
        AddAction("Feed", amount);
        Console.WriteLine($"{Name} was fed {amount} units. Food Level: {foodLevel}, Health: {health}");
    }

    public void DecreaseFood(int amount)
    {
        foodLevel = Math.Max(0, foodLevel - amount);
        if (foodLevel < 20)
        {
            health = Math.Max(0, health - 10);
            Console.WriteLine($"Warning: {Name} is hungry! Food: {foodLevel}, Health: {health}");
        }
    }

    public abstract string MakeSound();

    public override string GetInfo()
    {
        return $"{GetType().Name} '{Name}' [ID: {Id}] - Food: {foodLevel}, Health: {health}";
    }
}
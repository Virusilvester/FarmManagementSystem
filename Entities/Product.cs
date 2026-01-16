public abstract class Product : Sellable
{
    private string name;
    private int quantity;
    private decimal pricePerUnit;

    protected Product(string name, int quantity, decimal pricePerUnit)
    {
        this.name = name;
        this.quantity = quantity;
        this.pricePerUnit = pricePerUnit;
    }

    public string Name => name;
    public int Quantity => quantity;
    public decimal PricePerUnit => pricePerUnit;

    public void SetQuantity(int value)
    {
        if (value < 0)
            throw new InvalidQuantityException("Quantity cannot be negative");
        quantity = value;
    }

    public decimal Sell(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidQuantityException("Sell quantity must be greater than 0");
        if (quantity > this.quantity)
            throw new InvalidQuantityException($"Cannot sell {quantity}. Only {this.quantity} available.");
        
        this.quantity -= quantity;
        return quantity * pricePerUnit;
    }

    public override string ToString()
    {
        return $"{name}: {quantity} units @ K{pricePerUnit} each";
    }
}

public class Milk : Product
{
    public Milk(int quantity) : base("Milk", quantity, 60.35m) { }
}

public class Eggs : Product
{
    public Eggs(int quantity) : base("Eggs", quantity, 2.95m) { }
}

public class Wool : Product
{
    public Wool(int quantity) : base("Wool", quantity, 95.00m) { }
}

public class Grain : Product
{
    public Grain(int quantity) : base("Grain", quantity, 35.00m) { }
}

public class CornCobs : Product
{
    public CornCobs(int quantity) : base("Corn Cobs", quantity, 25.72m) { }
}

public class Vegetables : Product
{
    public Vegetables(int quantity) : base("Vegetables", quantity, 45.54m) { }
}
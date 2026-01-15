public class Action
{
    public string ActionType { get; } // Feed, Harvest, Sell
    public DateTime Date { get; }
    public int Quantity { get; }

    public Action(string actionType, int quantity)
    {
        ActionType = actionType;
        Date = DateTime.Now;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd HH:mm} | {ActionType,-10} | Qty: {Quantity,3}";
    }
}
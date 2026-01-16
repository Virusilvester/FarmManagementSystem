public class Action
{
    private string actionType;
    private DateTime date;
    private int quantity;

    public Action(string actionType, int quantity)
    {
        this.actionType = actionType;
        this.date = DateTime.Now;
        this.quantity = quantity;
    }

    public string ActionType => actionType;
    public DateTime Date => date;
    public int Quantity => quantity;

    public override string ToString()
    {
        return $"[{date:yyyy-MM-dd HH:mm}] {actionType} - Quantity: {quantity}";
    }
}
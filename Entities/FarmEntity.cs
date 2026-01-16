public abstract class FarmEntity
{
    private static int nextId = 1;
    private string id;
    private string name;
    private List<Action> actionHistory;

    protected FarmEntity(string name)
    {
        this.id = "FE" + (nextId++);
        this.name = name;
        this.actionHistory = new List<Action>();
    }

    public string Id => id;
    public string Name => name;
    
    public void SetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty");
        name = value;
    }

    protected void AddAction(string actionType, int quantity)
    {
        actionHistory.Add(new Action(actionType, quantity));
    }

    public List<Action> GetActionHistory()
    {
        return new List<Action>(actionHistory);
    }

    public abstract Product Produce();
    public abstract string GetInfo();
}
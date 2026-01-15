public abstract class FarmEntity
{
    private static int _idCounter = 0;
    private string _id;
    private string _name;
    private List<Action> _actionHistory;

    protected FarmEntity(string name)
    {
        _id = GenerateId();
        _name = name;
        _actionHistory = new List<Action>();
    }

    private static string GenerateId()
    {
        return $"ENT{++_idCounter:D4}";
    }

    // Getters (no setters for id)
    public string Id => _id;
    public string Name => _name;
    
    public IReadOnlyList<Action> ActionHistory => _actionHistory.AsReadOnly();

    protected void AddAction(string actionType, int quantity)
    {
        _actionHistory.Add(new Action(actionType, quantity));
    }

    public abstract string Produce();
    public abstract string GetStatus();
}
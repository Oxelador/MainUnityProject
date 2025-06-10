public class Stat
{
    public StatName Name {  get; private set; }
    public string Description { get; private set; }
    private float _baseValue;
    private float _currentValue;

    public Stat(StatName name, string description, float baseValue)
    {
        Name = name;
        Description = description;
        this._baseValue = baseValue;
        _currentValue = this._baseValue;
    }

    public void AddValue(float value)
    {
        _currentValue += value;
    }

    public float GetValue()
    {
        return _currentValue;
    }

    public override string ToString()
    {
        return $"Stat - {Name}. Value - {_currentValue}.\n" +
            $"Description - {Description}.";
    }
}

public enum StatName
{
    Survivability,
    Armor,
    Strength,
    CritChance,
    CritMultiplier
}

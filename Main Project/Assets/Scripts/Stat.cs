namespace oxi
{
    public class Stat
    {
        public StatName Name { get; private set; }
        public string Description { get; private set; }

        float baseValue;
        float currentValue;

        public Stat(StatName name, string description, float baseValue)
        {
            Name = name;
            Description = description;
            this.baseValue = baseValue;
            currentValue = this.baseValue;
        }

        public void AddValue(float value)
        {
            currentValue += value;
        }

        public float GetValue()
        {
            return currentValue;
        }

        public override string ToString()
        {
            return $"Stat - {Name}. Value - {currentValue}.\n" +
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
}

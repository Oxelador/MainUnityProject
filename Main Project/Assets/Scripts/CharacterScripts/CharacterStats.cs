using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Tooltip("Survivability: 1 survivability = 5 health points (HP)")]
    public float Survivability;
    [Tooltip("Armor: reduces damage taken by 1 for each point of armor, but no more than 35% of maximum health")]
    public float Armor;
    [Tooltip("Strength: 1 strength = 2 damage")]
    public float Strength;
    [Tooltip("Crit Chance: chance to increase damage based on crit damage multiplier")]
    public float CritChance;
    [Tooltip("Crit Multiplier: increases damage on critical hit")]
    public float CritMultiplier;

    public List<Stat> Stats { get; private set; } = new List<Stat>();

    void Awake()
    {
        Stats.Add(new Stat(StatName.Survivability, "1 survivability = 5 health point(HP)", Survivability));
        Stats.Add(new Stat(StatName.Armor, "reduces damage taken by 1 for each point of armor, but no more than 35% of maximum health", Armor));
        Stats.Add(new Stat(StatName.Strength, "1 strength = 2 damage", Strength));
        Stats.Add(new Stat(StatName.CritChance, "chance to increase damage based on crit damage multiplier", CritChance));
        Stats.Add(new Stat(StatName.CritMultiplier, "increases damage on critical hit", CritMultiplier));

        //DisplayStats();
    }

    public void AddValue(StatName statName, float value)
    {
        Stats.Find(stat => stat.Name == statName).AddValue(value);
    }

    public float GetStatValueByName(StatName statName)
    {
        if (statName == StatName.Survivability)
        {
            return Stats.Find(stat => stat.Name == statName).GetValue() * 5;
        }
        else if (statName == StatName.Armor)
        {
            return Stats.Find(stat => stat.Name == statName).GetValue() * 1;
        }
        else if (statName == StatName.Strength)
        {
            return Stats.Find(stat => stat.Name == statName).GetValue() * 2;
        }
        else if (statName == StatName.CritChance)
        {
            return Stats.Find(stat => stat.Name == statName).GetValue() * 1;
        }
        else if (statName == StatName.CritMultiplier)
        {
            return Stats.Find(stat => stat.Name == statName).GetValue() * 1;
        }
        else
        {
            Debug.LogError("Stat not found: " + statName);
            return 0;
        }

    }

    public void DisplayStats()
    {
        foreach (var stat in Stats)
        {
            Debug.Log(stat.ToString());
        }
    }
}

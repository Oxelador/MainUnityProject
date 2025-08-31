using System.Collections.Generic;
using UnityEngine;

namespace oxi
{
    public class CharacterStats : MonoBehaviour
    {
        public List<Stat> Stats { get; private set; } = new List<Stat>();

        [Tooltip("Survivability: 1 survivability = 5 health points (HP)")]
        public float survivability;
        [Tooltip("Armor: reduces damage taken by 1 for each point of armor, but no more than 35% of maximum health")]
        public float armor;
        [Tooltip("Strength: 1 strength = 2 damage")]
        public float strength;
        [Tooltip("Crit Chance: chance to increase damage based on crit damage multiplier")]
        public float critChance;
        [Tooltip("Crit Multiplier: increases damage on critical hit")]
        public float critMultiplier;

        void Awake()
        {
            Stats.Add(new Stat(StatName.Survivability, "1 survivability = 5 health point(HP)", survivability));
            Stats.Add(new Stat(StatName.Armor, "reduces damage taken by 1 for each point of armor, but no more than 35% of maximum health", armor));
            Stats.Add(new Stat(StatName.Strength, "1 strength = 2 damage", strength));
            Stats.Add(new Stat(StatName.CritChance, "chance to increase damage based on crit damage multiplier", critChance));
            Stats.Add(new Stat(StatName.CritMultiplier, "increases damage on critical hit", critMultiplier));

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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public List<BaseStat> statsList = new List<BaseStat>();

    private void Start()
    {
        foreach (var stat in statsList)
        {
            stat.Initialize();
        }
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public BaseStat GetStat(BaseStatType statType)
    {
        return statsList.Find(x=>x.StatType == statType);
    }

    public void DisplayStats()
    {
        Debug.Log($"Health: {GetStat(BaseStatType.Health)}");
        Debug.Log($"Strength: {GetStat(BaseStatType.Strength)}");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDesctiption { get; set; }
    public int FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDesctiption)
    {
        BaseAdditives = new List<StatBonus>();
        BaseValue = baseValue;
        StatName = statName;
        StatDesctiption = statDesctiption;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(BaseAdditives.Find(x=>x.BonusValue == statBonus.BonusValue));
    }

    public int CalculateStatValue()
    {
        this.FinalValue = 0;
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }

    public override string ToString()
    {
        return FinalValue.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseStatEnum
{
    Health,
    Strength
}

[Serializable]
public class BaseStat
{
    public List<StatBonus> BaseAdditives = new List<StatBonus>();
    public int BaseValue;
    public BaseStatEnum StatName;

    private int FinalValue;

    public void Initialize()
    {
        if(BaseAdditives == null)
        {
            BaseAdditives = new List<StatBonus>();
        }
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

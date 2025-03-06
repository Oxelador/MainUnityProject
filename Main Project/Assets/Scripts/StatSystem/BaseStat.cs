using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseStatType { Health, Strength }

[Serializable]
public class BaseStat
{
    public List<StatBonus> BaseAdditives = new List<StatBonus>();
    public float BaseValue;
    public BaseStatType StatType;

    public float FinalValue { get; private set; }

    public void Initialize()
    {
        if(BaseAdditives == null)
        {
            BaseAdditives = new List<StatBonus>();
        }
        else
        {
            CalculateStatValue();
        }
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
        CalculateStatValue();
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(BaseAdditives.Find(x=>x.BonusValue == statBonus.BonusValue));
        CalculateStatValue();
    }

    private float CalculateStatValue()
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

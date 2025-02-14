using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<BaseStat> stats = new List<BaseStat>();

    private void Awake()
    {
        foreach (var stat in stats)
        {
            stat.Initialize();
        }
    }

    public void AddStatBonus(List<BaseStat> baseStats)
    {
        //�������� ������ ������, ���������� �� ����
        foreach (BaseStat baseStat in baseStats)
        {
            // ���� �� ������ ������ ������ �������
            // ���������� ����� �� ����� � ���� ��� �����,
            stats.Find(x=> x.StatName == baseStat.StatName)
                // ��������� ��� � ������ �������� ������
                .AddStatBonus(new StatBonus(baseStat.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> baseStats)
    {
        //�������� ������ ������, ���������� �� ����
        foreach (BaseStat baseStat in baseStats)
        {
            // ���� �� ������ ������ ������ �������
            // ���������� ����� �� ����� � ���� ��� �����,
            stats.Find(x => x.StatName == baseStat.StatName)
                // ������� ��� �� ������ �������� ������
                .RemoveStatBonus(new StatBonus(baseStat.BaseValue));
        }
    }

    public BaseStat GetStat(BaseStatEnum statName)
    {
        return stats.Find(x=>x.StatName == statName);
    }

    public void DisplayStats()
    {
        Debug.Log($"Health: {GetStat(BaseStatEnum.Health)}");
        Debug.Log($"Strength: {GetStat(BaseStatEnum.Strength)}");
    }
}

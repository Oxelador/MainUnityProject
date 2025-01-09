using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<BaseStat> stats = new List<BaseStat>();

    [Header("Health")]
    [SerializeField] private int _healthValue;
    [SerializeField] private string _healthDescription;

    [Header("Strength")]
    [SerializeField] private int _strengthValue;
    [SerializeField] private string _strengthDescription;

    private void Awake()
    {
        stats.Add(new BaseStat(_healthValue, "Health", _healthDescription));
        stats.Add(new BaseStat(_strengthValue, "Strength", _strengthDescription));
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

    public void DisplayStats()
    {
        Debug.Log($"Health: {stats[0]}");
        Debug.Log($"Strength: {stats[1]}");
    }
}

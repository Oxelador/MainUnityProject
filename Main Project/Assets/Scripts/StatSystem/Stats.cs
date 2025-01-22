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
        //получаем список статов, проходимся по нему
        foreach (BaseStat baseStat in baseStats)
        {
            // берём из нашего списка статов элемент
            // сравниваем статы по имени и если они равны,
            stats.Find(x=> x.StatName == baseStat.StatName)
                // довабляем его в список бонусных статов
                .AddStatBonus(new StatBonus(baseStat.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> baseStats)
    {
        //получаем список статов, проходимся по нему
        foreach (BaseStat baseStat in baseStats)
        {
            // берём из нашего списка статов элемент
            // сравниваем статы по имени и если они равны,
            stats.Find(x => x.StatName == baseStat.StatName)
                // удаляем его из список бонусных статов
                .RemoveStatBonus(new StatBonus(baseStat.BaseValue));
        }
    }

    public void DisplayStats()
    {
        Debug.Log($"Health: {stats[0]}");
        Debug.Log($"Strength: {stats[1]}");
    }
}

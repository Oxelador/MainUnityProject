using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    Stats stats;

    void Start()
    {
        stats = GetComponent<Stats>();       
    }

    public void ConsumeItem(ConsumableItemData item)
    {
        if(item.ItemModifier)
        {
            item.ItemPrefab.GetComponent<IConsumable>().Consume(stats);
        }
        else
        {
            item.ItemPrefab.GetComponent<IConsumable>().Consume();
        }
    }
}

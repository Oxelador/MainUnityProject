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
        GameObject itemToSpawn = Instantiate(item.ItemPrefab, gameObject.transform.position, Quaternion.identity);
        if(item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(stats);
        }
        else
        {
            itemToSpawn.GetComponent<IConsumable>().Consume();
        }
    }
}

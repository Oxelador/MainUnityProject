using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePotion : MonoBehaviour, IConsumable
{
    public void Consume()
    {
        Debug.Log("You drink a health potion.");
    }

    public void Consume(Stats stats)
    {

    }
}

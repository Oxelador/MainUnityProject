using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{
    public void Consume()
    {
        Debug.Log("You drink a potion.");
    }

    public void Consume(Stats stats)
    {
        Debug.Log("You drink a modifier potion.");
    }
}

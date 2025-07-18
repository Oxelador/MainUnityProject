using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (this.gameObject.tag == "Gold")
            {
                Bag.OnGoldAdd?.Invoke(1);
                Debug.Log("Gold picked up! Current gold: " + Bag.OnGoldAdd.GetPersistentEventCount());
            }
            else if (this.gameObject.tag == "Perk")
            {
                
            }
            Destroy(gameObject);
        }
    }
}

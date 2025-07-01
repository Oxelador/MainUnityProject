using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();

    public float dropForce = 300f;

    List<Loot> GetDroppedItems()
    {
        int randomNumber = Random.Range(1, 101); // 1-100
        List<Loot> droppedItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                if(item.lootName == "Gold")
                {
                    int goldAmount = Random.Range(1, 5); // Random gold amount between 1 and 5
                    for(int i = 0; i < goldAmount; i++)
                    {
                        droppedItems.Add(item); // Add gold multiple times based on the amount
                    }
                }
                droppedItems.Add(item);
            }
        }
        if (droppedItems.Count > 0)
        {
            return droppedItems;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedLoot = GetDroppedItems();
        if(droppedLoot != null)
        {
            foreach (var loot in droppedLoot)
            {
                GameObject lootGameObject = Instantiate(loot.lootPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);

                // animation drop effect or particle effect can be added here
                Vector3 dropDirection = new Vector3(Random.Range(-1f, 1f), 1f, -1f);
                lootGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
                
            }
        }
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRange = 0.1f;
    public ItemData ItemData;

    private SphereCollider myCollider;

    private bool isEquipped = false;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEquipped) return;

        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        if(!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }

    public void EquipItem()
    {
        isEquipped = true;
        myCollider.enabled = false;
    }

    public void UnequipItem()
    {
        isEquipped = false;
    }

    public void DropItem()
    {
        isEquipped = false;
        myCollider.enabled = true;
    }
}

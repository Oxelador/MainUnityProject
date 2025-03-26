using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public ItemData ItemData;

    private bool isEquipped = false;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        if (isEquipped) return;

        if (Player.Instance.AddToPlayerInventory(ItemData))
        {
            Destroy(this.gameObject);
        }
    }

    public void EquipItem()
    {
        isEquipped = true;
    }

    public void UnequipItem()
    {
        isEquipped = false;
    }

    public void DropItem()
    {
        isEquipped = false;
    }
}

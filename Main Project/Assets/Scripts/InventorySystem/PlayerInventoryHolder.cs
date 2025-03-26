using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;

    // method for sending request when pressing "backpack button"
    public void DisplayRequest()
    {
        OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem);
    }

    public bool AddToInventory(ItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }
}

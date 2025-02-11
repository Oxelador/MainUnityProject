using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
        SaveLoad.OnLoadGame += LoadInventory;
    }

    protected override void LoadInventory(SaveData data)
    {
        // check the save data for this specific chest inventory, and if it exists, loat it in.
        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }


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

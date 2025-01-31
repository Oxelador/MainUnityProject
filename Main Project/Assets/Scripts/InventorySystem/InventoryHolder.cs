using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        SaveLoad.OnLoadGame += LoadInventory;

        primaryInventorySystem = new InventorySystem(inventorySize);
    }

    protected abstract void LoadInventory(SaveData saveData);
}

[Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;

    public InventorySaveData(InventorySystem invSystem)
    {
        this.InvSystem = invSystem;
    }
}

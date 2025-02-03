using System;
using UnityEngine;

[Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [NonSerialized] private ItemData _itemData; // Reference to the data
    [SerializeField] private int _itemID = -1;
    [SerializeField] private int _stackSize; // Current stack size - how name of the data do we have?

    // Public getter for private fields
    public ItemData ItemData => _itemData;
    public int StackSize => _stackSize;

    public InventorySlot(ItemData sourse, int amount) // Constructor to make a occupied inventory slot.
    {
        _itemData = sourse;
        _itemID = _itemData.ID;
        _stackSize = amount;
    }

    public InventorySlot() // Constructor to make an empty inventory slot.
    {
        CrearSlot();
    }

    public void CrearSlot() // Clear the slot.
    {
        _itemData = null;
        _itemID = -1;
        _stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assigns an item to the slot
    {
        if(_itemData == invSlot.ItemData) // Does the slot contain the same item? Add to stack if so.
        {
            AddToStack(invSlot.StackSize);
        }
        else // Override slot with the inventory slot that we're passing in.
        {
            _itemData = invSlot.ItemData;
            _itemID = _itemData.ID;
            _stackSize = 0;
            AddToStack(invSlot._stackSize);
        }
    }

    public void UpdateInventorySlot(ItemData data, int amount) // Updates slot directly.
    {
        _itemData = data;
        _itemID = _itemData.ID;
        _stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) // Would there be enouhg room in the stack for the amount we're trying to add.
    {
        amountRemaining = ItemData.MaxStackSize - _stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
        
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (_itemData == null || _itemData != null && _stackSize + amountToAdd <= _itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(_stackSize <= 1) // Is there enough to actually split? If not return false.
        {
            splitStack = null;
            return false;
        }
        
        int halfStack = Mathf.RoundToInt(_stackSize / 2); // Get half the stack.
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(_itemData, halfStack); // Creates a copy of this slot with half the stack size.
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<Database>("Database");
        _itemData = db.GetItem(_itemID);
    }
}

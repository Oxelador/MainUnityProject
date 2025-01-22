using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData; // Reference to the data
    [SerializeField] private int stackSize; // Current stack size - how name of the data do we have?

    // Public getter for private fields
    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData sourse, int amount) // Constructor to make a occupied inventory slot.
    {
        itemData = sourse;
        stackSize = amount;
    }

    public InventorySlot() // Constructor to make an empty inventory slot.
    {
        CrearSlot();
    }

    public void CrearSlot() // Clear the slot.
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assigns an item to the slot
    {
        if(itemData == invSlot.ItemData) // Does the slot contain the same item? Add to stack if so.
        {
            AddToStack(invSlot.StackSize);
        }
        else // Override slot with the inventory slot that we're passing in.
        {
            itemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(ItemData data, int amount) // Updates slot directly.
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) // Would there be enouhg room in the stack for the amount we're trying to add.
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
        
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(stackSize <= 1) // Is there enough to actually split? If not return false.
        {
            splitStack = null;
            return false;
        }
        
        int halfStack = Mathf.RoundToInt(stackSize / 2); // Get half the stack.
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); // Creates a copy of this slot with half the stack size.
        return true;
    }
}

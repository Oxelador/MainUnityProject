using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int _secondaryInventorySize;
    [SerializeField] protected InventorySystem _secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => _secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;


    protected override void Awake()
    {
        base.Awake();

        _secondaryInventorySystem = new InventorySystem(_secondaryInventorySize);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            OnPlayerBackpackDisplayRequested?.Invoke(_secondaryInventorySystem);
        }
    }

    public bool AddToInventory(ItemData data, int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if(_secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }
}

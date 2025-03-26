using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    public Health Health { get; private set; }
    public Stats Stats { get; private set; }
    public CharacterWeaponController CharacterWeaponController { get; private set; }
    private ConsumableController consumableController;
    private PlayerInventoryHolder playerInventoryHolder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Health = GetComponent<Health>();
        Stats = GetComponent<Stats>();
        CharacterWeaponController = GetComponent<CharacterWeaponController>();
        consumableController = GetComponent<ConsumableController>();
        playerInventoryHolder = GetComponent<PlayerInventoryHolder>();
    }
    public void EquipItem(ItemData itemToEquip)
    {
        CharacterWeaponController.EquipWeapon((EquipmentItemData)itemToEquip);
    }

    public void ConsumeItem(ItemData itemToConsume)
    {
        consumableController.ConsumeItem((ConsumableItemData)itemToConsume);
    }

    public bool AddToPlayerInventory(ItemData itemToAdd)
    {
        return playerInventoryHolder.AddToInventory(itemToAdd, 1);
    }
}


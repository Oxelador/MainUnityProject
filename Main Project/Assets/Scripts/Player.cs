using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    public Health Health { get; private set; }
    private CharacterWeaponController characterWeaponController;
    private ConsumableController consumableController;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Health = GetComponent<Health>();
        characterWeaponController = GetComponent<CharacterWeaponController>();
        consumableController = GetComponent<ConsumableController>();
    }
    public void EquipItem(ItemData itemToEquip)
    {
        characterWeaponController.EquipWeapon((EquipmentItemData)itemToEquip);
    }

    public void ConsumeItem(ItemData itemToConsume)
    {
        consumableController.ConsumeItem((ConsumableItemData)itemToConsume);
    }
}


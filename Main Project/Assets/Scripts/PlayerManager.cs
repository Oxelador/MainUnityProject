using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; set; }
    private CharacterWeaponController characterWeaponController;
    private ConsumableController consumableController;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

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


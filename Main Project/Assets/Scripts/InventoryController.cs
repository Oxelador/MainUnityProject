using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private CharacterWeaponController _playerWeaponController;
    private ConsumableController _consumableController;
    public Item sword;
    public Item PotionLog;

    private void Start()
    {
        _playerWeaponController = GetComponent<CharacterWeaponController>();
        _consumableController = GetComponent<ConsumableController>();

        List<BaseStat> swordStats = new List<BaseStat>
        {
            new BaseStat(5, "Strength", "Strength Point (SP)")
        };
        sword = new Item(swordStats, "test_sword");
        Debug.Log($"Item: {sword.ObjectSlug} with stats {sword.Stats[0].StatName} {sword.Stats[0].BaseValue} created.");

        PotionLog = new Item(new List<BaseStat>(), "PotionLog", "description for potion", "Drink", "PotionLogName", false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            _playerWeaponController.EquipWeapon(sword);
            _consumableController.ConsumeItem(PotionLog);
        }
    }
}

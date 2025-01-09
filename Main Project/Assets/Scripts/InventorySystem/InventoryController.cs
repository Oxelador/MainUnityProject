using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    private CharacterWeaponController _playerWeaponController;
    private ConsumableController _consumableController;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _playerWeaponController = GetComponent<CharacterWeaponController>();
        _consumableController = GetComponent<ConsumableController>();
    }

    
}

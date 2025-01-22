using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventoryController : MonoBehaviour
{
    private CharacterWeaponController _enemyWeaponController;
    public EquipmentItemData weapon;

    private void Start()
    {
        _enemyWeaponController = GetComponent<CharacterWeaponController>();
        StartCoroutine(EquipEnemyWeapon());
    }

    private IEnumerator EquipEnemyWeapon()
    {
        yield return new WaitForSeconds(.5f);
        _enemyWeaponController.EquipWeapon(weapon);
    }
}

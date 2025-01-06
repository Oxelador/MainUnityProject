using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventoryController : MonoBehaviour
{
    private CharacterWeaponController _enemyWeaponController;
    public Item weapon;

    private void Start()
    {
        _enemyWeaponController = GetComponent<CharacterWeaponController>();
        List<BaseStat> weaponStats = new List<BaseStat>
        {
            new BaseStat(2, "Strength", "Strength Point (SP)")
        };
        weapon = new Item(weaponStats, "no_weapon_dummy");
        Debug.Log($"Item: {weapon.ObjectSlug} with stats {weapon.Stats[0].StatName} {weapon.Stats[0].BaseValue} created.");
        StartCoroutine(EquipEnemyWeapon());
    }

    private IEnumerator EquipEnemyWeapon()
    {
        yield return new WaitForSeconds(.5f);
        _enemyWeaponController.EquipWeapon(weapon);
    }
}

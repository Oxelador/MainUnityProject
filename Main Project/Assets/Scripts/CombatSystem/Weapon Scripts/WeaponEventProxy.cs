using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventProxy : MonoBehaviour
{
    public GameObject weaponSpot;
    private IWeapon _weapon;

    private void Awake()
    {
        _weapon = weaponSpot.GetComponentInChildren<IWeapon>();
    }

    public void StartAttack()
    {
        if (_weapon != null)
        {
            _weapon.StartAttack();
        }
    }

    public void EndAttack()
    {
        if (_weapon != null)
        {
            _weapon.EndAttack();
        }
    }
}

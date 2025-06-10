using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventProxy : MonoBehaviour
{
    [SerializeField] private MeleeWeapon _weapon;

    public void EndAttack()
    {
        if (_weapon != null)
        {
            _weapon.EndAttack();
        }
    }
}

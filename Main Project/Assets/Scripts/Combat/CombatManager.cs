using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject _weaponSpot;
    private Stats _stats;
    private IWeapon _equipedWeapon;
    private Collider _equipedWeaponCollider;
    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _stats = GetComponent<Stats>();

        _equipedWeapon = _weaponSpot.GetComponentInChildren<IWeapon>();

        if( _equipedWeapon != null)
        {
            // add Strength(stats[1]) from weapon to our character
            _stats.stats[1].AddStatBonus(new StatBonus((int)_equipedWeapon.WeaponDamage));
            _equipedWeaponCollider = _equipedWeapon.GetComponent<Collider>();
            _equipedWeapon.Damage = _stats.stats[1].CalculateStatValue();
            Debug.Log(this.gameObject.name + " weapon damage is " + _equipedWeapon.Damage);
        }

        _stats.DisplayStats();

    }

    private void Update()
    {
        if(tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _equipedWeapon.Attack();
            }
        }
        else if(tag == "Enemy")
        {
            if (!_enemyController.IsCaughtUp)
            {
                _equipedWeapon.Attack();
            }
        }
    }

    public void EnableCollider()
    {
        _equipedWeaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        _equipedWeaponCollider.enabled = false;
    }
}


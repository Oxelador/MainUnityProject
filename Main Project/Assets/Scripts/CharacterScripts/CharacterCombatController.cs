using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(WeaponEventProxy))]
public class CharacterCombatController : MonoBehaviour
{
    public GameObject _weaponSpot;
    public IWeapon _weapon;
    public float fireRate = 4f;
    
    public bool IsDead { get; set; } = false;

    private Animator _animator;
    private CharacterStats _characterStats;
    private float timeToFire;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterStats = GetComponent<CharacterStats>();
        _weapon = _weaponSpot.GetComponentInChildren<IWeapon>();
    }

    void Update()
    {
        if(IsDead) return;
        if (this.CompareTag("Player") && Input.GetKey(KeyCode.Mouse0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            _weapon.PerformAttack(CalculateDamage(), null);
            _animator.SetTrigger("attack");
        }
    }

    public float CalculateDamage()
    {
        float damage = _characterStats.GetStatValueByName(StatName.Strength);
        float critChance = _characterStats.GetStatValueByName(StatName.CritChance);
        float critDamage = damage * (1f + _characterStats.GetStatValueByName(StatName.CritMultiplier) / 100f);

        if(Random.Range(0f, 100f) <= critChance)
        {
            return critDamage;
        }
        return damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatController : MonoBehaviour
{
    public MeleeWeapon _weapon;

    private CharacterStats _characterStats;
    public Animator animator;

    void Start()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (this.CompareTag("Player") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _weapon.PerformAttack(CalculateDamage());
            animator.SetTrigger("attack");
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

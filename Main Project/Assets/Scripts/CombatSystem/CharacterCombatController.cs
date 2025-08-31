using oxi;
using UnityEngine;

public abstract class CharacterCombatController : MonoBehaviour
{
    [Header("References")]
    protected WeaponSlotManager weaponSlotManager;
    protected CharacterStats characterStats;
    protected AnimatorHandler animatorHandler;
    protected Health health;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public virtual void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        characterStats = GetComponent<CharacterStats>();
        health = GetComponent<Health>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    public virtual void Start()
    {
        if (rightWeapon != null)
        {
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }
        if (leftWeapon != null)
        {
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
    }

    public float CalculateDamage()
    {
        float damage = characterStats.GetStatValueByName(StatName.Strength);
        float critChance = characterStats.GetStatValueByName(StatName.CritChance);
        float critDamage = damage * (1f + characterStats.GetStatValueByName(StatName.CritMultiplier) / 100f);

        if (Random.Range(0f, 100f) <= critChance)
        {
            return critDamage;
        }
        return damage;
    }

}

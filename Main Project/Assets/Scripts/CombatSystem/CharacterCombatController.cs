using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterStats))]
public abstract class CharacterCombatController : MonoBehaviour
{
    [Header("References")]
    protected WeaponSlotManager weaponSlotManager;
    protected CharacterStats characterStats;
    protected AnimatorHandler animatorHandler;
    protected Health health;
    protected IWeapon weapon;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public virtual void Awake()
    {
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        characterStats = GetComponent<CharacterStats>();
        health = GetComponent<Health>();
    }

    public virtual void Start()
    {
        animatorHandler = GetComponent<AnimatorHandler>();

        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

        weapon = rightWeapon.modelPrefab.gameObject.GetComponent<IWeapon>();
    }

    public abstract void PerformAttack();

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

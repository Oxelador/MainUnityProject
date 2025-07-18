using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(WeaponEventProxy))]
public abstract class CharacterCombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _weaponSpot;
    protected CharacterStats _characterStats;
    protected Animator _animator;
    protected Health _health;
    public IWeapon _weapon;

    public virtual void Start()
    {
        _characterStats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _weapon = _weaponSpot.GetComponentInChildren<IWeapon>();
    }

    public abstract void PerformAttack();

    public float CalculateDamage()
    {
        float damage = _characterStats.GetStatValueByName(StatName.Strength);
        float critChance = _characterStats.GetStatValueByName(StatName.CritChance);
        float critDamage = damage * (1f + _characterStats.GetStatValueByName(StatName.CritMultiplier) / 100f);

        if (Random.Range(0f, 100f) <= critChance)
        {
            return critDamage;
        }
        return damage;
    }

}

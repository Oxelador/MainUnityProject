using System;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public event Action<float> UpdateHealth;

    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            if (value <= 0)
            {
                _maxHealth = 1;
            }
        }
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
                UpdateHealth?.Invoke(_currentHealth);
            }
            if (this.gameObject.tag == "Player")
            {
                float _currentHealthAsPercantage = (float)_currentHealth / _maxHealth;
                UpdateHealth?.Invoke(_currentHealthAsPercantage);
            }
        }
    }

    private float _maxHealth;
    private float _currentHealth;
    private float _armor;
    private bool isDead = false;

    private Animator _animator;
    private Collider _collider;
    private CharacterStats _characterStats;


    void Start()
    {
        if(gameObject.tag == "Player")
        {
            _animator = GetComponentInChildren<Animator>();
        }
        else
        {
            _animator = GetComponent<Animator>();
        }
        _collider = GetComponent<Collider>();
        _characterStats = GetComponent<CharacterStats>();

        _maxHealth = _characterStats.GetStatValueByName(StatName.Survivability);
        _currentHealth = _maxHealth;
        _armor = _characterStats.GetStatValueByName(StatName.Armor);
        //Debug.Log($"{this.gameObject.name} Health is {_maxHealth}");
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Prevent further damage if already dead

        float armorHealthLimitPercentage = (_maxHealth / 100) * 35;
        float amountAfterArmor = amount - _armor;

        if (amountAfterArmor >= armorHealthLimitPercentage)
        {
            amountAfterArmor = armorHealthLimitPercentage;
        }

        if (_currentHealth <= 0 || amountAfterArmor > _currentHealth)
        {
            Death();
        }
        else
        {
            CurrentHealth -= amountAfterArmor;
            _animator.SetTrigger("isHitted");
            Debug.Log(this.name + " take " + amountAfterArmor + " damage.");

        }

    }

    public void Heal(float amount)
    {
        if (amount >= _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }

        CurrentHealth += amount;
    }

    void Death()
    {
        if (isDead) return; // Prevent multiple death calls

        isDead = true;
        UpdateHealth?.Invoke(0);
        Debug.Log(this.name + " is die!");

        if (tag == "Enemy")
        {
            LootBag lootBag = GetComponent<LootBag>();
            lootBag?.InstantiateLoot(transform.position);
            _collider.enabled = false;
            GetComponent<EnemyController>().enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<CharacterCombatController>().IsDead = true;
        }

        _animator.SetTrigger("death");
    }
}

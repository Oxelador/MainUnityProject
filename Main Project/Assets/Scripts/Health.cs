using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public event Action<float> UpdateHealth;
    private float _maxHealth;
    private float _currentHealth;
    private Stats _stats;
    private Animator _animator;
    private Collider _collider;

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
            if(this.gameObject.tag == "Player")
            {
                float _currentHealthAsPercantage = (float) _currentHealth / _maxHealth;
                UpdateHealth?.Invoke(_currentHealthAsPercantage);
            }
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _stats = GetComponent<Stats>();
        _maxHealth = _stats.stats[0].CalculateStatValue();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if( _currentHealth <= 0 || amount > _currentHealth)
        {
            Death();
        }
        else
        {
            CurrentHealth -= amount;
            Debug.Log(this.name + " take " + amount + " damage.");
            
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
        UpdateHealth?.Invoke(0);
        Debug.Log(this.name + " is die!");

        if(tag == "Enemy")
        {
            _collider.enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<PlayerWeaponController>().enabled = false;
        }

        _animator.SetBool("death", true);
    }
}

using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public float Damage { get; set; }

    public GameObject owner;

    private bool _hasDealtDamage;
    private bool _isAttacking;

    private void Awake()
    {
        owner = transform.root.gameObject;
    }

    public void PerformAttack(float damage)
    {
        Damage = damage;
        _isAttacking = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isAttacking || _hasDealtDamage) return;
        if (other.gameObject == owner) return;

        var health = other.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(Damage);
            _hasDealtDamage = true;
        }
    }

    public void EndAttack()
    {
        _hasDealtDamage = false;
        _isAttacking = false;
    }
}

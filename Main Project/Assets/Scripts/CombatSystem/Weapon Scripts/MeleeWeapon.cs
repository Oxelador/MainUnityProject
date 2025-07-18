using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public float Damage { get; set; }

    private GameObject _owner;
    private bool _isAttacking;
    private HashSet<GameObject> _damagedEnemies = new HashSet<GameObject>();

    private void Awake()
    {
        _owner = transform.root.gameObject;
    }

    public void PerformAttack(float damage, Transform target)
    {
        Damage = damage;
        _isAttacking = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _owner)
            return;

        if (!_isAttacking)
            return;

        if (collider.gameObject.CompareTag("Enemy"))
        {
            if(_damagedEnemies.Contains(collider.gameObject))
                return;

            var health = collider.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(Damage);
                _damagedEnemies.Add(collider.gameObject);
            }
        }
    }

    public void StartAttack()
    {
    }

    public void EndAttack()
    {
        _isAttacking = false;
        _damagedEnemies.Clear();
    }
}

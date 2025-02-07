using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public List<BaseStat> Stats { get; set; }
    private Animator _animator;
    private GameObject _owner;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _owner = transform.root.gameObject;
    }

    public void PerformAttack()
    {
        _animator.SetTrigger("melee_attack");
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject == _owner || target.gameObject == target.gameObject) return;

        Health targetHealth = target.gameObject.GetComponent<Health>();
        if(targetHealth != null )
        {
            if(target.tag == "Enemy" || target.tag == "Player")
            {
                targetHealth.TakeDamage(Stats[0].CalculateStatValue());
            }
        }
    }
}

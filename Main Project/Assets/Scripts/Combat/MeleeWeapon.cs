using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public List<BaseStat> Stats { get; set; }
    [SerializeField] private float _timeAttack;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        GetComponent<Collider>().enabled = false;
    }

    public void PerformAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _animator.SetBool("melee_attack", true);
        yield return new WaitForSeconds(_timeAttack);
        _animator.SetBool("melee_attack", false);
    }

    private void OnTriggerEnter(Collider target)
    {
        Health targetHealth = target.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            //targetHealth.TakeDamage(Damage);
        }

    }
}

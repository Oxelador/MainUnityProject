using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public List<BaseStat> WeaponStatList { get; set; }
    public float CurrentDamage { get; set; }

    private List<Collider> _collidersTouched = new List<Collider>();
    private Animator _animator;
    private GameObject _owner;
    private bool _isAttacking;
    private float _attackDuration;

    private void Start()
    {
        _isAttacking = false;
    }

    private void Update()
    {
        _owner = transform.root.gameObject;

        if (_owner.tag == "Player" && _animator == null)
        {
            _animator = _owner.GetComponent<Animator>();

            foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "MeleeAttack_OneHanded 1")
                {
                    _attackDuration = clip.length;
                    break;
                }
            }
        }
    }

    public void PerformAttack(float damage)
    {
        CurrentDamage = damage;
        StartAttack();
        StartCoroutine("ResetAttack");
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_attackDuration);
        EndAttack();
    }

    private void StartAttack()
    {
        _animator.SetTrigger("melee_attack");
        _isAttacking = true;
        _collidersTouched.Clear();
    }

    private void EndAttack()
    {
        _isAttacking = false;
        ProcessDamage();
    }

    private void ProcessDamage()
    {
        foreach (var target in _collidersTouched)
        {
            Health targetHealth = target.gameObject.GetComponent<Health>();
            if (targetHealth != null)
            {
                if ((target.tag == "Enemy" && _owner.tag == "Player")
                    || (target.tag == "Player" && _owner.tag == "Enemy"))
                {
                    targetHealth.TakeDamage(CurrentDamage);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject == _owner || _owner == null || !_isAttacking) return;

        if (!_collidersTouched.Contains(target))
        {
            _collidersTouched.Add(target);
        }
    }
}

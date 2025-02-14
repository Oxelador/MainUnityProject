using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public List<BaseStat> Stats { get; set; }
    private Animator _animator;
    private GameObject _owner;
    private bool _isAttacking;
    private float _attackDuration;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _isAttacking = false;

        Debug.Log($"{this.gameObject.name} owner is {_owner}");

    }

    private void Update()
    {
        _owner = transform.root.gameObject;

        if (_owner.tag == FindObjectOfType<PlayerController>().transform.tag)
        {
            foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "melee_attack")
                {
                    _attackDuration = clip.length;
                    break;
                }
            }
        }
    }

    public void PerformAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("melee_attack");
        StartCoroutine("ResetAttack");
    }

    private IEnumerable ResetAttack()
    {
        yield return new WaitForSeconds(_attackDuration);
        _isAttacking = false;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject == _owner || _owner == null || !_isAttacking) return;

        Health targetHealth = target.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            if ((target.tag == "Enemy" && _owner.tag == "Player")
                || (target.tag == "Player" && _owner.tag == "Enemy"))
            {
                targetHealth.TakeDamage(Stats[0].CalculateStatValue());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : CharacterCombatController
{
    private PlayerController _playerController;

    private int _attackCount = 0;

    public override void Start()
    {
        base.Start();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_health.IsDead) return;

        bool isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        _weapon.PerformAttack(CalculateDamage(), null);

        bool isAttackMoving = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack in Move");

        if (!_playerController.IsMoving && !isAttackMoving)
        {
            _attackCount++;

            if (_attackCount == 1)
            {
                _animator.SetInteger("intAttackPhase", 1);
            }
        }
        else if (_playerController.IsMoving && !isAttackMoving)
        {
            _animator.SetTrigger("attack");
        }
    }

    public void CheckAttackPhase()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash(0)"))
        {
            if (_attackCount > 1)
            {
                _animator.SetInteger("intAttackPhase", 2);
            }
            else
            {
                ResetAttackPhase();
            }
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash(1)"))
        {
            if (_attackCount > 2)
            {
                _animator.SetInteger("intAttackPhase", 3);
            }
            else
            {
                ResetAttackPhase();
            }
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash(2)"))
        {
            if (_attackCount >= 3)
            {
                ResetAttackPhase();
            }
        }
    }

    private void ResetAttackPhase()
    {
        _attackCount = 0;
        _animator.SetInteger("intAttackPhase", 0);
    }
}

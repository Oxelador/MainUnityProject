using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public LayerMask aggroLayerMask;
    public float aggroRange = 10;

    private Player _player;
    private Stats _enemyStats;
    private NavMeshAgent _enemyNavAgent;
    private Animator _animator;
    private Collider[] _withinAggroColliders;

    void Start()
    {
        _enemyStats = GetComponent<Stats>();
        _enemyNavAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRange, aggroLayerMask);
        if (_withinAggroColliders.Length > 0)
        {
            ChasePlayer(_withinAggroColliders[0].GetComponent<Player>());
        }

        if(_player != null)
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void ChasePlayer(Player player)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > aggroRange)
        {
            _enemyNavAgent.ResetPath();
            _animator.SetBool("run", false);
            CancelInvoke("PerformAttack");
            return;
        }

        _enemyNavAgent.SetDestination(player.transform.position);
        _player = player;

        if (_enemyNavAgent.remainingDistance <= _enemyNavAgent.stoppingDistance)
        {
            _animator.SetBool("run", false);
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 2f);
            }
        }
        else
        {
            _animator.SetBool("run", true);
            CancelInvoke("PerformAttack");
        }
    }

    void PerformAttack()
    {
        _animator.SetTrigger("melee_attack");
        _player.Health.TakeDamage(_enemyStats.GetStat(BaseStatType.Strength).FinalValue);
    }
}

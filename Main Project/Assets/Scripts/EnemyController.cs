using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private NavMeshAgent _agent;
    private Animator _anim;
    private bool _isCaughtUp;

    public bool IsCaughtUp { get { return _isCaughtUp; } }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _agent.destination = _player.position;
        _isCaughtUp = Vector3.Distance(transform.position, _player.position) > _agent.stoppingDistance;

        if (_isCaughtUp)
        {
            _anim.SetBool("run", true);
        }
        else
        {
            _anim.SetBool("run", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : CharacterCombatController
{
    private Transform player;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void PerformAttack()
    {
        weapon.PerformAttack(CalculateDamage(), player);
    }
}

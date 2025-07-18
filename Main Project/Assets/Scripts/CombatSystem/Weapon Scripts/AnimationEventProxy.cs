using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    public GameObject weaponSpot;
    private IWeapon _weapon;

    // References if object is a player
    private PlayerCombatController _playerCombatController;
    private PlayerController _playerController;

    private void Awake()
    {
        _weapon = weaponSpot.GetComponentInChildren<IWeapon>();
        if(this.gameObject.tag == "Player")
        {
            _playerCombatController = GetComponent<PlayerCombatController>();
            _playerController = GetComponent<PlayerController>();
        }
    }

    public void StartAttack()
    {
        if (_weapon != null)
        {
            _weapon.StartAttack();
            _playerController.LockMovement();
        }
    }

    public void EndAttack()
    {
        if (_weapon != null)
        {
            _weapon.EndAttack();
            _playerCombatController.CheckAttackPhase();
            _playerController.UnlockMovement();
        }
    }
}

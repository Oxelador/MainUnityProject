using oxi;
using UnityEngine;

public class PlayerCombatController : CharacterCombatController
{
    InputHandler inputHandler;
    public string lastAttack;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        inputHandler = GetComponent<InputHandler>();
    }

    public void HandleWeaponCombo()
    {
        if (inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);
            if (lastAttack == rightWeapon.oh_light_attack_01)
            {
                animatorHandler.PlayTargetAnimation(rightWeapon.oh_light_attack_02, true);
            }
        }
    }

    public void HandleLightAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.oh_light_attack_01, true);
        lastAttack = rightWeapon.oh_light_attack_01;
    }

    public void HandleHeavyAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.oh_heavy_attack_01, true);
        lastAttack = rightWeapon.oh_heavy_attack_01;
    }
}

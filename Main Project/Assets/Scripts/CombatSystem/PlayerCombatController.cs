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
            if (lastAttack == rightWeapon.OH_Light_Attack_1)
            {
                animatorHandler.PlayTargetAnimation(rightWeapon.OH_Light_Attack_2, true);
            }
        }
    }

    public void HandleLightAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.OH_Light_Attack_1, true);
        lastAttack = rightWeapon.OH_Light_Attack_1;
    }

    public void HandleHeavyAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.OH_Heavy_Attack_1, true);
        lastAttack = rightWeapon.OH_Heavy_Attack_1;
    }
}

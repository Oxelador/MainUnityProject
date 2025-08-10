using Unity.VisualScripting;

public class PlayerCombatController : CharacterCombatController
{

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void PerformAttack()
    {
        
    }

    public void HandleLightAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.OH_Light_Attack_1, true);
    }

    public void HandleHeavyAttack()
    {
        animatorHandler.PlayTargetAnimation(rightWeapon.OH_Heavy_Attack_1, true);
    }
}

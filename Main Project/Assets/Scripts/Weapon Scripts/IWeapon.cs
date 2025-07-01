using UnityEngine;

public interface IWeapon
{
    public float Damage { get; set; }

    public void PerformAttack(float damage, Transform target);

    public void StartAttack();

    public void EndAttack();
}

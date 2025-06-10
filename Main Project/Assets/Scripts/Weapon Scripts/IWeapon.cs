public interface IWeapon
{
    public float Damage { get; set; }

    public void PerformAttack(float damage);
}

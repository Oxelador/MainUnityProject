using System.Collections.Generic;

public interface IWeapon
{
    List<BaseStat> WeaponStatList { get; set; }
    float CurrentDamage { get; set; }
    void PerformAttack(float damage);
}


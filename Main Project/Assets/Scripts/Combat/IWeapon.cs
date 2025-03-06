using System.Collections.Generic;

public interface IWeapon
{
    List<BaseStat> WeaponStatList { get; set; }
    void PerformAttack();
}


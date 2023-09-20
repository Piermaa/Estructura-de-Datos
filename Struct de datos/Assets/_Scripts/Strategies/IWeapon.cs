using UnityEngine;

public interface IWeapon : IPickupable
{
    GameObject GameObject { get; }
    WeaponStats WeaponStats { get; }
    int RemainingBullets { get; }
    void Reload();
    void Shoot(WeaponHolder weaponHolder);
    void OnWeaponMagazineEmpty();
}
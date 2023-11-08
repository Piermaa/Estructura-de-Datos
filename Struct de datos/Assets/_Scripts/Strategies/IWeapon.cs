using UnityEngine;

public interface IWeapon : IPickupable
{
    public float TravelSpeed { get; }
    bool Thrown { get; }
    GameObject GameObject { get; }
    WeaponStats WeaponStats { get; }
    int RemainingBullets { get; }
    void Reload();
    void Shoot(WeaponHolder weaponHolder);
    void OnWeaponMagazineEmpty();
    void Throw();
}
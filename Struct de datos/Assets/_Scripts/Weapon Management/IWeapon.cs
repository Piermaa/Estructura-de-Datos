using UnityEngine;

public interface IWeapon
{
    Transform WeaponTransform { get; }
    int RemainingBullets { get; }
    int Damage { get; }
    float BulletTravelDistance { get; }
    void Shoot();
    //por ahi on OnPickup, o sonido y particula y listo
}
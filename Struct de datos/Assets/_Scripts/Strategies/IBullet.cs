using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet : IPoolable
{
    BulletStats BulletStats { get; }
    IWeapon Owner { get; }
    void InitBullet(IWeapon owner, Vector3 shootDir);
    void OnCollisionEnter(Collision col);
    void Travel();
}

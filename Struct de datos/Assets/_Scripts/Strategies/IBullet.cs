using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet : IPoolable
{
    BulletStats BulletStats { get; }
    void InitBullet(Vector3 shootDir);
    void OnCollisionEnter(Collision col);
    void Travel();
}

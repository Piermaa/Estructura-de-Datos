using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Stats/BulletStats")]
public class BulletStats : ScriptableObject
{
    [field: SerializeField] public float TravelSpeed { get; private set; } = 5f;
    [field: SerializeField] public float MaxLifetime { get; private set; } = 1;
    [field: SerializeField] public int MaxPoolableBullets { get; private set; } = 10;
}

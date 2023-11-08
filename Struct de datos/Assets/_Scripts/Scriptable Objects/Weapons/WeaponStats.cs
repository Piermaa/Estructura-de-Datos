using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponStats", menuName = "Stats/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    
    [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    
    [field: SerializeField] public Sprite WeaponSprite { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; } = 0.3f;
    [field: SerializeField] public float TravelSpeed { get; private set; } = 15f;
    [field: SerializeField] public int BulletsPerShot { get; private set; } = 1;
    [field: SerializeField] public int MagSize { get; private set; } = 10;
    [field: SerializeField] public int Damage { get; private set; } = 1;
}

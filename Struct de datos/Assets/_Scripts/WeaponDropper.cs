using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponDropper : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    public void DropRandomWeapon()
    {
        var chance = Random.Range(0, 4);

       // print("a ver si dropeo");
        if (chance == 0)
        {
            var selectedWeapon = Random.Range(0, weapons.Length);

         //   print("dropeo");
            
            Instantiate(weapons[selectedWeapon], gameObject.transform.position, Quaternion.identity);
        }
        else
        {
           // print("No dropeo");
        }
    }
}

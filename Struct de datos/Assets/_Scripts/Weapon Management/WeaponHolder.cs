using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    
    private const string WEAPON_TAG = "Weapon"; 
    private IWeapon _equippedWeapon;
    //Todo: Reemplazar por la Pila propia
    private PilaTF<IWeapon> _pickedWeapons=new();

    private void Awake()
    {
        _pickedWeapons.Init(50);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EquipWeapon();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShootEquippedWeapon();
        }
    }

    private void ShootEquippedWeapon()
    {
        if (!_pickedWeapons.isEmpty()) 
        {
            _equippedWeapon.Shoot();
        }
    }

    private void EquipWeapon()
    {
        if (_equippedWeapon!=null)
        {
            UnequipCurrentWeapon();
        }
        
        if (!_pickedWeapons.isEmpty())
        {
            _equippedWeapon = _pickedWeapons.Pop();
            _equippedWeapon.WeaponTransform.gameObject.SetActive(true);
        }
    }

    private void UnequipCurrentWeapon()
    {
        _equippedWeapon.WeaponTransform.gameObject.SetActive(false);
    }

    private void PickupWeapon(IWeapon weaponToPickUp)
    {
        _pickedWeapons.Add(weaponToPickUp);
        
        //print(_pickedWeapons.);
        
        weaponToPickUp.WeaponTransform.SetParent(weaponParent);
        weaponToPickUp.WeaponTransform.localPosition = Vector3.zero;
            
        weaponToPickUp.WeaponTransform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(WEAPON_TAG))
        {
            PickupWeapon(other.GetComponent<IWeapon>());
        }
    }
}

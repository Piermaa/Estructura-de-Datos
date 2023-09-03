using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    
    private const string WEAPON_TAG = "Weapon"; 
    private IWeapon _equippedWeapon = null;
    //Todo: Reemplazar por la Pila propia
   
    private PilaTF<IWeapon> _pickedWeapons=new();

    private void Awake()
    {
        _pickedWeapons.Init(50);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShootEquippedWeapon();
        }
    }

    private void ShootEquippedWeapon()
    {
        if (!_pickedWeapons.isEmpty() && _equippedWeapon!=null) 
        {
            _equippedWeapon.Shoot();
        }
    }

    private void EquipWeaponNextWeapon()
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
        //EL ARMA ACTUAL SE METE EN EL STACK
        if (_equippedWeapon!=null)
        {
            _equippedWeapon.WeaponTransform.gameObject.SetActive(false);
            _pickedWeapons.Add(_equippedWeapon);
        }
        //SE CAMBIA EL ARMA ACTUAL
        _equippedWeapon = weaponToPickUp;
        //print(_pickedWeapons.);
        
        weaponToPickUp.WeaponTransform.SetParent(weaponParent);
        weaponToPickUp.WeaponTransform.localPosition = Vector3.zero;
        weaponToPickUp.WeaponTransform.localRotation = Quaternion.identity;
        weaponToPickUp.WeaponTransform.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(WEAPON_TAG))
        {
            PickupWeapon(other.GetComponent<IWeapon>());
        }
    }
}

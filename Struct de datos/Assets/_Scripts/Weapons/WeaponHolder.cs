using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class WeaponHolder : MonoBehaviour
{
    //----PUBLIC PROPERTIES--------
    public ObjectPool EquippedWeaponBulletPool => bulletPool;
    public IWeapon EquippedWeapon => _equippedWeapon;

    //----PRIVATE PROPERTIES-------- 
    private IWeapon _equippedWeapon = null;
    private ObjectPool bulletPool;

    private const int MAX_STACK_SIZE = 50;
    private PilaTF<IWeapon> _pickedWeapons=new();


    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    #region  MONOBEHAVIOUR_CALLBACKS

    private void OnEnable()
    {
        Weapon.OnWeaponPickup += PickupWeapon;
        Weapon.OnWeaponOutOfBullets += UnequipWeaponAndTryEquipNextWeapon;
    }
    private void OnDisable()
    {
        Weapon.OnWeaponPickup -= PickupWeapon;
        Weapon.OnWeaponOutOfBullets -= UnequipWeaponAndTryEquipNextWeapon;
    }

    private void Awake()
    {
        _pickedWeapons.Init(MAX_STACK_SIZE);
        bulletPool = GetComponent<ObjectPool>();
    }
    #endregion

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //SUSCRIPTO A WEAPON -> ONMAGAZINEEMPTY
    private void UnequipWeaponAndTryEquipNextWeapon()
    {
        //PRIMERO FLETAR EL ARMA QUE YA NO SIRVE Y BORRAR SU POOL DE BALAS.
        bulletPool.EmptyPool();
        Destroy(_equippedWeapon.GameObject);
        //ASUIMR QUE EL JUGADOR YA NO TIENE ARMAS.
        _equippedWeapon = null;

        //PARA LUEGO INTENTAR EQUIPAR EL SIGUIENTE EN EL STACK
        _equippedWeapon = _pickedWeapons.Pop();

        //SI ES QUE HAY SI NO EQUIPPED WEAPON QUEDA VACIO
        if (_equippedWeapon != null)
        {
            InitBulletPool(_equippedWeapon);
            _equippedWeapon.GameObject.SetActive(true);
        }
    } 

    //SUSCRIPTO A WEAPON -> PICKUP
    private void PickupWeapon(IWeapon weaponToPickUp)
    {
        _pickedWeapons.Add(weaponToPickUp);

        //SI HABIA UN ARMA EQUIPADA DE ANTES, SE GUARDA DEVUELTA EN EL STACK Y SE BORRA SU POOL TEMPORALMENTE ASI EL ARMA NUEVA NO SE LO USA
        if (_equippedWeapon != null)
        {
            _equippedWeapon.GameObject.SetActive(false);
            bulletPool.EmptyPool();
        }
        //SE CAMBIA EL ARMA ACTUAL
        _equippedWeapon = weaponToPickUp;

        weaponToPickUp.GameObject.transform.SetParent(this.transform);
        weaponToPickUp.GameObject.transform.localPosition = Vector3.zero;
        weaponToPickUp.GameObject.transform.localRotation = Quaternion.identity;
        weaponToPickUp.GameObject.SetActive(true);

        //SE INICIALIZA EL POOL DE BALAS PARA EL ARMA
        InitBulletPool(weaponToPickUp);
    }

    private void InitBulletPool(IWeapon currentEquippedWeapon)
    {
        IPoolable objectToPool = currentEquippedWeapon.WeaponStats.BulletPrefab.GetComponent<IPoolable>();
        bulletPool.CreatePool(objectToPool, objectToPool.GameObject.GetComponent<Bullet>().BulletStats.MaxPoolableBullets);
    }
}

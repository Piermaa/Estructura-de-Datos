using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    [SerializeField] private Weapon defaultWeapon;

    [SerializeField] private WeaponsUI weaponsUI;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickupWeaponSFX;
    [SerializeField] private AudioClip _throwWeaponSFX;
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
        
        //Inicializo la UI para asegurar que exista
        weaponsUI.InitializeUI(MAX_STACK_SIZE);

        EquipDefaultWeapon();
    }
    #endregion

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    private void EquipDefaultWeapon()
    {
        Weapon defaultWeapon = Instantiate(this.defaultWeapon, this.transform);
        PickupWeapon(defaultWeapon.GetComponent<IWeapon>());
    }

    //SUSCRIPTO A WEAPON -> ONMAGAZINEEMPTY
    private void UnequipWeaponAndTryEquipNextWeapon()
    {
        //PRIMERO FLETAR EL ARMA QUE YA NO SIRVE Y BORRAR SU POOL DE BALAS.
        bulletPool.EmptyPool();
        
        //TIRO EL ARMA            
        _equippedWeapon.Throw();
        _audioSource.PlayOneShot(_throwWeaponSFX);

        //Borro el arma de la UI
        weaponsUI.DeleteWeaponFromUI();

        //LUEGO INTENTAR EQUIPAR EL SIGUIENTE EN EL STACK
        _equippedWeapon = _pickedWeapons.Pop();

        //SI ES QUE HAY SI NO EQUIPPED WEAPON QUEDA VACIO. Esto ya no pasa porque tiene arma por defecto
        if (_equippedWeapon != null)
        {
            InitBulletPool(_equippedWeapon);
            _equippedWeapon.GameObject.SetActive(true);
        }
    } 

    //SUSCRIPTO A WEAPON -> PICKUP
    private void PickupWeapon(IWeapon weaponToPickUp)
    {
        _audioSource.PlayOneShot(_pickupWeaponSFX);
        _pickedWeapons.Add(weaponToPickUp);

        //Agrego el arma a la UI
        weaponsUI.AddWeaponToUI(weaponToPickUp.WeaponStats.WeaponSprite);
        weaponsUI.UpdateBulletsText(weaponToPickUp.WeaponStats.MagSize);
        
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

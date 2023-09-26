using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour, IWeapon
{
    //----PUBLIC PROPERTIES--------
    public GameObject GameObject => this.gameObject;
    public WeaponStats WeaponStats => weaponStats;
    public int RemainingBullets => remainingBullets;

    //----PROTECTED VARS---------
    protected int remainingBullets;

    //----PRIVATE VARS---------
    [SerializeField] private WeaponStats weaponStats;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Start()
    {
        Reload();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(this);
        }
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //-------IPICKUPABLE--------
    public static event Action<IWeapon> OnWeaponPickup; //-------> WEAPONHOLDER SE SUSCRIBE PARA VER QUE ARMA SE METE AHI
    public void Pickup(IPickupable weaponBeingPickedUp)
    {
        if (weaponBeingPickedUp is IWeapon)
            OnWeaponPickup?.Invoke(this);
    }

    //--------IWEAPON-----------

    //COMO SE DESCARTAN LAS ARMAS EL RELOAD ES SOLO PARA INICIALIZAR EL TAMAÑO DEL POOL
    public void Reload()
    {
        remainingBullets = weaponStats.MagSize;
    }
    public virtual void Shoot(WeaponHolder weaponHolder)
    {
        //esto se va a overridear por cada arma
    }
    public static event Action OnWeaponOutOfBullets; //-------> WEAPONHOLDER SE SUSCRIBE PARA VER CUANDO DESCARTAR EL ARMA
    public void OnWeaponMagazineEmpty()
    {
        OnWeaponOutOfBullets?.Invoke();
    }
}

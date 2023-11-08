using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour, IWeapon
{
    //----PUBLIC PROPERTIES--------
    public bool Thrown => _thrown;
    public float TravelSpeed => weaponStats.TravelSpeed;
    public GameObject GameObject => this.gameObject;
    public WeaponStats WeaponStats => weaponStats;
    public int RemainingBullets => remainingBullets;

    //----PROTECTED VARS---------
    protected AudioSource _audioSource;
    protected int remainingBullets;
    protected bool _thrown;
    //----PRIVATE VARS---------
    [SerializeField] private WeaponStats weaponStats;

    private Vector3 _thrownAngle = new Vector3(0,45,90);
    private Vector3 _pickedUpAngle = new Vector3(0,180,33);

    private Quaternion _thrownRotation;
    private Quaternion _pickedUpRotation;
    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Start()
    {
        _thrownRotation = Quaternion.Euler(_thrownAngle);
        _pickedUpRotation = Quaternion.Euler(_pickedUpAngle);
        
        _audioSource = GetComponent<AudioSource>();
        Reload();
    }

    private void Update()
    {
        if (_thrown)
        {
            Travel();
            transform.GetChild(0).Rotate(Vector3.right, weaponStats.RotateSpeed*Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_thrown && other.CompareTag("Player"))
        {
            Pickup(this);
            transform.GetChild(0).localRotation = _pickedUpRotation;
        }

        if (_thrown && other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(weaponStats.ThrownDamage);
            gameObject.SetActive(false);
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

    public void Throw()
    {
        _thrown = true;
        transform.parent = null;
        transform.GetChild(0).localRotation = _thrownRotation;
    }

    public void Travel()
    {
        transform.position +=  Time.deltaTime * TravelSpeed * transform.forward;
    }
}

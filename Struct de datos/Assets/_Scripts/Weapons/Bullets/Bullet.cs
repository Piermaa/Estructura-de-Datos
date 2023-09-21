using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IBullet
{
    //------PUBLIC PROPERTIES-------
    public GameObject GameObject => this.gameObject;
    public BulletStats BulletStats => bulletStats;
    public IWeapon Owner => owner;

    //------PRIVATE PROPERTIES-------
    [SerializeField] private BulletStats bulletStats;
    private IWeapon owner;
    private Vector3 shootDir;
    private float lifeTimer = 0;


    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################

    private void Update()
    {
        lifeTimer -= Time.deltaTime;

        //Bullet Destroy
        if (lifeTimer <= 0)
        {
            OnPoolableObjectDisable();
        }

        Travel();
    }

    //Por ahora colisiona con cualquier cosa
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(owner.WeaponStats.Damage);
        }

        OnPoolableObjectDisable();
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //-----IBULLET--------
    public void InitBullet(IWeapon owner, Vector3 shootDir)
    {
        this.owner = owner;
        lifeTimer = bulletStats.MaxLifetime;
        this.shootDir = shootDir;
    }
    public void Travel()
    {
        transform.position += shootDir * Time.deltaTime * bulletStats.TravelSpeed;
    }

    //-----IPOOLABLE--------

    //No se destruye porque esto va pooleado. Despues habria que destruir el pool si el jugador/enemigo que tenga un pool de por ejemplo,
    //balas, asi no quedan pools vivitos por ahi que no pertenecen a nadie.
    public void OnPoolableObjectDisable()
    {
        lifeTimer = bulletStats.MaxLifetime;
        gameObject.SetActive(false);
    }
}

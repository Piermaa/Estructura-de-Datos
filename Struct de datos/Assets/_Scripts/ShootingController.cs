using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ShootingController : MonoBehaviour
{
    private ObjectPool _bulletPool;
    [SerializeField] private GameObject _bullet;

    private float _shootTimer = 0;
    [SerializeField] private float fireRateTotalmenteArbitrario = 0.3f;
    [SerializeField] private int maxBulletsToPoolTotalmenteArbitrario = 10;

    private void Start()
    {
        _bulletPool = GetComponent<ObjectPool>();
        InitBulletPool();

    }

    private void Update()
    {
        _shootTimer += Time.deltaTime;
        ListenForShootInput();
    }

    private void InitBulletPool()
    {
        //Esto se cambia segun la bala / max bullets que permita el arma quien sea se encargue de eso
        _bulletPool.InitPool(_bullet, maxBulletsToPoolTotalmenteArbitrario);
    }

    private void ListenForShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        //El que haga las armas/weapons, despues tiene que incluir un param de firerate en cada arma y aca en vez de pasar un valor arbitrario
        //iria currentEquippedWeapon.FireRate)
        if (_shootTimer >= fireRateTotalmenteArbitrario)
        {
            _bulletPool.GetPooledObject();
            _shootTimer = 0;
        }
    }
}

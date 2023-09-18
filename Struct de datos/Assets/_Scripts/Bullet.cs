using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletManager bulletManager
    {
        set { _bulletManager = value; }
    }

    private bool hit = false;
    private BulletManager _bulletManager;
    
    [SerializeField] private float _speed;
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");

        //var t = transform;

        //t.position += t.up* Time.deltaTime * _speed;

        //hit = t.position.y >= 100;

        Travel();

        if (hit)
        {
            _bulletManager.RemoveBullet(this);
            Destroy(gameObject);
        }
    }

    private void Travel() => transform.position += transform.forward * Time.deltaTime * _speed;
}

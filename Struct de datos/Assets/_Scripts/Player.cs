using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.position += new Vector3(1, 0) * x * Time.deltaTime * _speed;


        if (Input.GetMouseButtonDown(0))
        {
            var b = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
            b.bulletManager = _bulletManager;
           _bulletManager.AddBullet(b);
        }
    }
}

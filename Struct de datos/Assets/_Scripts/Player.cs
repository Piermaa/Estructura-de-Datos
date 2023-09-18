using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShootable
{
    private Camera _camera;
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnTransform;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Aim();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        var b = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
        b.bulletManager = _bulletManager;
        _bulletManager.AddBullet(b);
        b.transform.SetPositionAndRotation(_bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
    }

    public void Aim()
    {
        Vector3 positionOnScreen = _camera.WorldToViewportPoint(transform.position);

        Vector3 mouseOnScreen = _camera.ScreenToViewportPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2((a.y - b.y) * 9, (a.x - b.x) * 16) * Mathf.Rad2Deg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IPoolable
{
    public GameObject GameObject => this.gameObject;

    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private float _lifeTimer = 0;

    //-----UNITY FUNCTIONS--------
    private void Start()
    {
        _lifeTimer = _lifeTime;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        var t = transform;
        t.position += t.forward * Time.deltaTime * _speed;

        _lifeTimer -= Time.deltaTime;

        //Bullet Destroy
        if (_lifeTimer <= 0)
        {
            OnPoolableObjectDisable();
        }
    }

    //Por ahora colisiona con cualquier cosa
    private void OnCollisionEnter(Collision collision)
    {
        OnPoolableObjectDisable();
    }

    //-----IPOOLABLE--------

    //No se destruye porque esto va pooleado. Despues habria que destruir el pool si el jugador/enemigo que tenga un pool de por ejemplo,
    //balas, asi no quedan pools vivitos por ahi que no pertenecen a nadie.
    public void OnPoolableObjectDisable()
    {
        _lifeTimer = _lifeTime;
        gameObject.SetActive(false);
    }
}

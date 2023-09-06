using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.position += new Vector3(1, 0) * x * Time.deltaTime * _speed;
    }
}

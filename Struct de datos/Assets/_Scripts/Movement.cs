using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMovable
{
    #region PUBLIC_VARIABLES
    public float MoveSpeed => _moveSpeed;
    #endregion

    #region PRIVATE_VARIABLES
    [SerializeField] private float _moveSpeed;

    //Despues pasar el componente a actor y que lo herede de ahi despues
    private Rigidbody actorRB;
    #endregion

    private void Start()
    {
        actorRB = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // Calculate inputs
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        actorRB.velocity = new Vector3(xMovement, 0, zMovement) * Time.deltaTime * _moveSpeed;

    }
}

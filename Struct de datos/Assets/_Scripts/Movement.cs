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
    #endregion

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        // Calculate inputs
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        // Calculate movement
        transform.Translate(Time.deltaTime * xMovement * _moveSpeed * Vector3.right);

        transform.Translate(Time.deltaTime * yMovement * _moveSpeed * new Vector3(0, 0, 1));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor, IMovable
{
    #region PUBLIC_VARIABLES
    public float MoveSpeed => _moveSpeed;
    #endregion

    #region PRIVATE_VARIABLES
    [SerializeField] private float _moveSpeed;
    private float xMovement = 0;
    private float zMovement = 0;
    private bool isMoving = false;
    #endregion

    private void Update()
    {
        ListenForMoveInput();
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
        else HaltMovement();
    }

    private void ListenForMoveInput()
    {
        xMovement = Input.GetAxis("Horizontal");
        zMovement = Input.GetAxis("Vertical");

        if (xMovement != 0 || zMovement != 0)
            isMoving = true;
        else isMoving = false;
    }

    #region IMOVABLE_METHODS
    public void Move()
    {
        actorRB.velocity = new Vector3(xMovement, 0, zMovement) * Time.deltaTime * _moveSpeed;
    }
    private void HaltMovement()
    {
        actorRB.velocity = Vector3.zero;
        actorRB.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    #endregion
    [ContextMenu("/Kill")]
    public override void Die()
    {
        ActionsManager.InvokeAction(ActionKeys.PLAYER_DEATH_KEY);
        base.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor, IMoveable
{
    #region PUBLIC_VARIABLES
    public float MoveSpeed => _moveSpeed;
    #endregion

    #region PRIVATE_VARIABLES
    [SerializeField] private float _moveSpeed;
    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    #region IMOVABLE_METHODS
    public void Move()
    {
        // Calculate inputs
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        actorRB.velocity = new Vector3(xMovement, 0, zMovement) * Time.deltaTime * _moveSpeed;
    }
    #endregion
    [ContextMenu("/Kill")]
    public override void Die()
    {
        ActionsManager.InvokeAction(ActionKeys.PLAYER_DEATH_KEY);
        base.Die();
    }
}

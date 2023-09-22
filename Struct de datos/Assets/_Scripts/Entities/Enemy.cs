using System.Diagnostics.Tracing;
using UnityEngine;

public class Enemy: Actor
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float currentAttackCooldown;
    [SerializeField] private LayerMask hitteableLayer;
    [SerializeField] private int difficultyLevel;
    private WeaponDropper _weaponDropper;
    //Muchas se podrían poner en un posible enemystats (o actorstats yqc)
    #endregion
    
    #region UNITY_METHODS
    protected override void Start()
    {
        base.Start();
        currentAttackCooldown = attackCooldown;
        _weaponDropper = GetComponentInChildren<WeaponDropper>();
    }
    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        if (currentAttackCooldown < -1)
        {
            currentAttackCooldown = -1;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (currentAttackCooldown <= 0)
        {
            if (((1 << other.gameObject.layer) & hitteableLayer) != 0)
            {
                other.gameObject.GetComponent<Actor>()?.TakeDamage(damage);
            }
        }
    }
    #endregion

    public override void Die()
    {
        ActionsManager.InvokeAction(ActionKeys.ENEMY_DEATH_KEY);
        if(_weaponDropper != null) _weaponDropper.DropRandomWeapon();
        base.Die();
    }
    //Lógica de dijkstra
    
    
    //--------Management de enemigos--------
    //Deben depender de un factory
    //Deben generarse aleatoriamente
    //Deben almacenarse en una cola
    //Dicha cola deberá ordenarse por nivel de dificultad
    //Deberán generarse a medida que el jugador los elimina en algunos puntos concretos del mapa
}
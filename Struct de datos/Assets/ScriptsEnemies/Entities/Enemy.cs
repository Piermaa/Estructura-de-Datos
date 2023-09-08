using ScriptsEnemies.Entities;
using UnityEngine;

public class Enemy: Actor
{
    #region PUBLIC_PROPERTIES
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float currentAttackCooldown;
    [SerializeField] private LayerMask hitteableLayer;
    [SerializeField] private int difficultyLevel;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        base.Start();
        currentAttackCooldown = attackCooldown;
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
    
    //Lógica de dijkstra
    
    
    //--------Management de enemigos--------
    //Deben depender de un factory
    //Deben generarse aleatoriamente
    //Deben almacenarse en una cola
    //Dicha cola deberá ordenarse por nivel de dificultad
    //Deberán generarse a medida que el jugador los elimina en algunos puntos concretos del mapa
}
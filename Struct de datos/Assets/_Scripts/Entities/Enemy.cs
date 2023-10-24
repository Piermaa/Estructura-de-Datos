using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy: Actor, IElementoConPrioridad
{
    public int Priority => DifficultyLevel;
    
    #region PRIVATE_PROPERTIES
    [SerializeField] protected EnemyStats _stats;
    
    private int Damage => _stats.Damage;
    private int DifficultyLevel => _stats.DifficultyLevel;
    private float Speed => _stats.Speed;
    
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float currentAttackCooldown;
    [SerializeField] private LayerMask hitteableLayer;
    private WeaponDropper _weaponDropper;
    private NavMeshAgent _navMeshAgent;
    #endregion
    
    #region UNITY_METHODS
    protected override void Start()
    {
        base.Start();
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = Speed;
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

        ChaseTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (currentAttackCooldown <= 0)
        {
            if (((1 << other.gameObject.layer) & hitteableLayer) != 0)
            {
                other.gameObject.GetComponent<Actor>()?.TakeDamage(Damage);
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

    public void ChaseTarget()
    {
        if (_playerTransform == null)
            return;

        _navMeshAgent.SetDestination(_playerTransform.position);
    }

}
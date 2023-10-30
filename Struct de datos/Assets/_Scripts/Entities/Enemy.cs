using System;
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
    private ChaseABBTask _chaseAbbTask;
    #endregion
    
    #region UNITY_METHODS

    private void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        currentAttackCooldown = attackCooldown;
        _weaponDropper = GetComponentInChildren<WeaponDropper>();

        _chaseAbbTask = new ChaseABBTask(GameObject.FindGameObjectWithTag("Player")?.transform,
            GetComponent<NavMeshAgent>(), Speed);
    }

    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        if (currentAttackCooldown < -1)
        {
            currentAttackCooldown = -1;
        }
        
        _chaseAbbTask.Process();
       
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
        if (_weaponDropper != null) _weaponDropper.DropRandomWeapon();
        base.Die();
    }
}

public class ChaseABBTask : NodoABB
{
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    public ChaseABBTask(Transform playerTransform, NavMeshAgent nmAgent, float speed)
    {
        _playerTransform = playerTransform;
        _navMeshAgent = nmAgent;
        _navMeshAgent.speed = speed;
    }

    public override void Process()
    {
        _navMeshAgent.SetDestination(_playerTransform.position);
    }
}
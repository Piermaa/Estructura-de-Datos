using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

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
    [SerializeField] private CanAttackABBCheck _canAttackAbbCheck;
    
    private Dictionary<string, bool> _blackBoard=new();
    private WeaponDropper _weaponDropper;
    private NavMeshAgent _navMeshAgent;

  //  private ChaseABBTask chaseAbbTask;
    
    private ABB _abb;
    #endregion
    
    #region UNITY_METHODS

    private void Awake()
    {
        ///////// CAN ATTACK
        /// move                atack
    }

    protected override void Start()
    {
        base.Start();
        currentAttackCooldown = attackCooldown;
        _weaponDropper = GetComponentInChildren<WeaponDropper>();

        _abb = new ABB(); //se instancia el arbol
        _abb.InicializarArbol(); // se inicializa

        _canAttackAbbCheck.SetBlackBoard(ref _blackBoard);
        
        _abb._raiz = _canAttackAbbCheck;
        //_abb.AgregarElem( _canAttackAbbCheck,1);
        
         var chaseAbbTask = new ChaseABBTask(GameObject.FindGameObjectWithTag("Player")?.transform,
            GetComponent<NavMeshAgent>(), Speed,ref _blackBoard);

         _abb._raiz.hijoDer = chaseAbbTask;
         
    //    _abb.AgregarElem(chaseAbbTask,2);
    }

    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        if (currentAttackCooldown < -1)
        {
            currentAttackCooldown = -1;
        }
        
     //   print("soy la info de la raiz: " + _abb._raiz.hijoDer?.info);
        ABBOrders.preOrder(_abb._raiz);
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
[System.Serializable]
public class CanAttackABBCheck : NodoABB
{
    [SerializeField] private Transform _playerCheckOrigin;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius;
    private Dictionary<string, bool> _blackBoard;

    public void SetBlackBoard(ref Dictionary<string, bool> blackBoard)
    {
        _blackBoard = blackBoard;
        _blackBoard.Add(key, false);
    }

    public override void Process()
    {
        Debug.Log("Mestan provceanso");
        
        var cols = Physics.OverlapSphere(_playerCheckOrigin.position, _radius,_whatIsPlayer);
        
        foreach (var col in cols)
        {
            if (col.CompareTag("Player"))
            {
                _blackBoard[key] = true;
                return;
            }
        }
        
        _blackBoard[key] = false;
    }
}

public class ChaseABBTask : NodoABB
{
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    private Dictionary<string, bool> _blackBoard;
    public ChaseABBTask(Transform playerTransform, NavMeshAgent nmAgent, float speed, ref Dictionary<string,bool> blackBoard)
    {
        _playerTransform = playerTransform;
        _navMeshAgent = nmAgent;
        _navMeshAgent.speed = speed;
        _blackBoard = blackBoard;

        key = "CanAttack";
    }

    public override void Process()
    {
        if (!_blackBoard[key])
        {
            _navMeshAgent.SetDestination(_playerTransform.position);
        }

    }
}
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

    [SerializeField] private CanAttackABBCheck _canAttackAbbCheck;
    [SerializeField] private AttackABBTask _attackTask;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float attackCooldown;
    
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

        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        //currentAttackCooldown = attackCooldown;
        _weaponDropper = GetComponentInChildren<WeaponDropper>();

        _abb = new ABB(); //se instancia el arbol
        _abb.InicializarArbol(); // se inicializa

        _canAttackAbbCheck.SetBlackBoard(ref _blackBoard);

        // _abb._raiz = _canAttackAbbCheck;
        _abb.AgregarElem(ref _abb.raiz, _canAttackAbbCheck);

        var chaseAbbTask = new ChaseABBTask(_playerTransform, GetComponent<NavMeshAgent>(), Speed, ref _blackBoard);

        //  _abb._raiz.hijoDer = chaseAbbTask;
        _abb.AgregarElem(ref _abb.raiz, chaseAbbTask);

        _attackTask.SetParameters(_playerTransform.gameObject, GetComponent<Animator>(), ref _blackBoard);

        _abb.AgregarElem(ref _abb.raiz, _attackTask);
    }

    private void Update()
    {
        //currentAttackCooldown -= Time.deltaTime;
        //if (currentAttackCooldown < -1)
        //{
        //    currentAttackCooldown = -1;
        //}
        
     //   print("soy la info de la raiz: " + _abb._raiz.hijoDer?.info);
        ABBOrders.preOrder(_abb.raiz);
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
    private float _timerToAttack = 1;
    [SerializeField] private float _cooldownToAttack = 1;

    public void SetBlackBoard(ref Dictionary<string, bool> blackBoard)
    {
        _blackBoard = blackBoard;
        _blackBoard.Add(key, false);
    }

    public override void Process()
    {
        _timerToAttack -= Time.deltaTime;

        if (_timerToAttack <= 0)
        {
            var cols = Physics.OverlapSphere(_playerCheckOrigin.position, _radius, _whatIsPlayer);

            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                {
                    _blackBoard[key] = true;
                    _timerToAttack = _cooldownToAttack;
                    return;
                }
            }
        }

        _blackBoard[key] = false;
    }
}

public class ChaseABBTask : NodoABB
{
    #region Class Properties

    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    private Dictionary<string, bool> _blackBoard;

    #endregion

    public ChaseABBTask(Transform playerTransform, NavMeshAgent nmAgent, float speed, ref Dictionary<string,bool> blackBoard)
    {
        _playerTransform = playerTransform;
        _navMeshAgent = nmAgent;
        _navMeshAgent.speed = speed;
        _blackBoard = blackBoard;

        key = "CanAttack";
    }

    #region NodoABB Methods

    public override void Process()
    {
        if (!_blackBoard[key])
        {
            _navMeshAgent.SetDestination(_playerTransform.position);
        }
    }

    #endregion
}

[System.Serializable]
public class AttackABBTask : NodoABB
{
    #region Class Properties
    private int _damage;
    private IDamageable _player;
    [SerializeField] private Animator _animator;
    private Dictionary<string, bool> _blackBoard;
    #endregion

    public override void Process()
    {
        if (_blackBoard[key])
        {
            _player.TakeDamage(_damage);
            _animator.SetTrigger("AttackTrigger");
        }
    }

    public void SetParameters(GameObject player, Animator animator, ref Dictionary<string, bool> blackboard, int damage)
    {
        _player = player.GetComponent<IDamageable>();
        _animator = animator;
        _blackBoard = blackboard;
        _damage = damage;
    }
}
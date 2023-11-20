using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
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
    private GraphAM _levelNodeGraph;

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
        _levelNodeGraph = GameObject.FindGameObjectWithTag("NodeMap")?.GetComponent<NodeMap>().LevelMapGraph;

        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        //currentAttackCooldown = attackCooldown;
        _weaponDropper = GetComponentInChildren<WeaponDropper>();

        _abb = new ABB(); //se instancia el arbol
        _abb.InicializarArbol(); // se inicializa

        _canAttackAbbCheck.Initialize(ref _blackBoard, _playerTransform);

        // _abb._raiz = _canAttackAbbCheck;
        _abb.AgregarElem(ref _abb.raiz, _canAttackAbbCheck);

        var chaseAbbTask = new ChaseABBTask(_levelNodeGraph, this.gameObject.transform, _playerTransform, 
            GetComponent<NavMeshAgent>(), GetComponent<PathfinderComponent>(), Speed, ref _blackBoard);

        //  _abb._raiz.hijoDer = chaseAbbTask;
        _abb.AgregarElem(ref _abb.raiz, chaseAbbTask);

        _attackTask.SetParameters(_playerTransform.gameObject, GetComponent<Animator>(), ref _blackBoard, _stats.Damage);

        _abb.AgregarElem(ref _abb.raiz, _attackTask);
    }

    private void FixedUpdate()
    {
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
    [FormerlySerializedAs("player")] [SerializeField] private Transform _player;
    [SerializeField] private float _distanceToAttackPlayer;
    [SerializeField] private Transform _playerCheckOrigin;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius;
    private Dictionary<string, bool> _blackBoard;
    private float _timerToAttack = 1;
    [SerializeField] private float _cooldownToAttack = 1;

    public void Initialize(ref Dictionary<string, bool> blackBoard, Transform player)
    {
        _blackBoard = blackBoard;
        _blackBoard.Add(key, false);
        _player = player;
    }

    public override void Process()
    {
        _timerToAttack -= Time.deltaTime;

        if (_timerToAttack <= 0)
        {
            if (Vector3.Distance(_player.position,_playerCheckOrigin.position) < _distanceToAttackPlayer)
            {
                _blackBoard[key] = true;
                _timerToAttack = _cooldownToAttack;
                return;
            }

            // var cols = Physics.OverlapSphere(_playerCheckOrigin.position, _radius, _whatIsPlayer);
            //
            // foreach (var col in cols)
            // {
            //     if (col.CompareTag("Player"))
            //     {
            //         _blackBoard[key] = true;
            //         _timerToAttack = _cooldownToAttack;
            //         return;
            //     }
            // }
        }

        _blackBoard[key] = false;
    }
}

public class ChaseABBTask : NodoABB
{
    #region Class Properties

    private GraphAM _levelNodeGraph;
    private Transform _selfTransform;
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    private PathfinderComponent _pathfinderComp;
    private Dictionary<string, bool> _blackBoard;
    private const int AGGRESSION_RADIUS = 6;

    private float decisionTimer = 0;
    private const float DECISION_TIME = 1;

    #endregion

    public ChaseABBTask(GraphAM levelNodeGraph, Transform selfTransform, Transform playerTransform, NavMeshAgent nmAgent, 
        PathfinderComponent pathfComp, float speed, ref Dictionary<string,bool> blackBoard)
    {
        _levelNodeGraph = levelNodeGraph;
        _selfTransform = selfTransform;
        _playerTransform = playerTransform;
        _navMeshAgent = nmAgent;
        _navMeshAgent.speed = speed;
        _pathfinderComp = pathfComp;
        _blackBoard = blackBoard;

        key = "CanAttack";
    }

    #region NodoABB Methods

    public override void Process()
    {
        if (!_blackBoard[key])
        {
            //Cada 1 segundo revalua el camino, no lo hagan updatear todos los frames no sean cabezoides.
            decisionTimer += Time.deltaTime;
            if (decisionTimer > DECISION_TIME)
            {
                //De entre todos los nodos del mapa, se fija el nodo mas cercano a self para saber de donde sale
                int sourceNode = GetNodeClosestToTarget(_selfTransform, "Source");
                //Se fija el nodo mas cercano al objeto que quieras ir (jugador en este caso) para saber a cual nodo tiene que ir
                int targetNode = GetNodeClosestToTarget(_playerTransform, "Final");

                //Usa Dijkstra para encontrar el conjunto de Nodos que llevan mas rapido a ese nodo
                _pathfinderComp.Dijkstra(_levelNodeGraph, sourceNode);
                Node[] optimalPathToTarget = _pathfinderComp.GetOptimalPathToTarget(_levelNodeGraph, sourceNode, targetNode,
                    _pathfinderComp.distance, _levelNodeGraph.cantNodos, _levelNodeGraph.Etiqs, _pathfinderComp.nodos);

                //Self se va moviendo al proximo nodo de ese camino
                int nextNodeToMoveTo = 0;
                if (optimalPathToTarget.Length > 0)
                    nextNodeToMoveTo = optimalPathToTarget[1].NodeNumber;

                Transform targetPosition = _levelNodeGraph.Nodes[nextNodeToMoveTo].gameObject.transform;

                //Navmesh se encarga del resto por que saludos
                if (Vector3.Distance(_selfTransform.position, _playerTransform.position) <= AGGRESSION_RADIUS)
                {
                    _navMeshAgent.SetDestination(_playerTransform.position);
                }
                else _navMeshAgent.SetDestination(targetPosition.position);

                decisionTimer = 0;
            }
        }
    }


    private int GetNodeClosestToTarget(Transform targetObjectNearNode, string debugMode)
    {
        Node[] nodes = _levelNodeGraph.Nodes;
        int totalNodes = _levelNodeGraph.cantNodos;

        int targetNode = 0;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < totalNodes; i++)
        {
            float distance = (targetObjectNearNode.position - nodes[i].transform.position).sqrMagnitude;

            if (distance < nearestDistance)
            {
                targetNode = i;
                nearestDistance = distance;
            }
        }

        switch (debugMode)
        {
            case "Source":
                Debug.Log("Source node closest to self is " + targetNode + ". Using as origin.");
                break;
            case "Final":
                Debug.Log("Target Node closest to Player is " + targetNode + ". Using as destination.");
                break;
        }
        return targetNode;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //--Esto se pone en cualquier objeto que cree un pool de objetos IPoolable -> Ej el arma del jugador que crea balas
    //--Poner el objeto vacio que contenga los objetos pooleados dentro de la var poolFolder serializada en el inspector.
    //--TODO: Despues mas facil hago que se cree el padre que las almacena cuando se inicializa asi menos paja

    //------PUBLIC PROPERTIES-------
    public bool IsPoolInited
    {
        get { return isPoolInited; }
    }

    //------PRIVATE PROPERTIES-------
    private bool isPoolInited = false;

    private ColaTF<IPoolable> objectPool;
    private IPoolable objectToPool;
    private int poolSize = 10;

    [SerializeField] private Transform poolFolder;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Awake()
    {
        objectPool = new ColaTF<IPoolable>();

        if (poolFolder == null)
        {
            Debug.LogWarning("Recordatorio: Objects in pool " + this.name + " don't have a parent.");
        }
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    public void CreatePool(IPoolable objectToPool, int poolMaxSize = 10)
    {
        if (objectPool != null)
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolMaxSize;
            objectPool.InicializarCola(poolMaxSize);
            isPoolInited = true;
        }
        else Debug.LogWarning("Object is not a poolable object.");
    }

    public void EmptyPool()
    {
        foreach (IPoolable p_obj in objectPool)
        {
            if (p_obj != null)
            {
                Destroy(p_obj.GameObject);
            }
        }
        objectPool.Clear();
    }

    public IPoolable TryGetPooledObject(Vector3 position, Quaternion rotation)
    {
        IPoolable pooledObject = null;

        if (objectPool.Count < poolSize)
        {
            pooledObject = NewObject(position, rotation);
        }
        else
        {
            pooledObject = ReuseObject(position, rotation);
        }

        objectPool.Acolar(pooledObject);
        return pooledObject;
    }

    private IPoolable NewObject(Vector3 position, Quaternion rotation)
    {
        GameObject newObject = Instantiate(objectToPool.GameObject, position, rotation);
        IPoolable pooledObject = newObject.GetComponent<IPoolable>();
        pooledObject.GameObject.name = transform.root.name + "_" + objectToPool.GameObject.name + "_" + objectPool.Count;
        pooledObject.GameObject.transform.SetParent(poolFolder);

        return pooledObject;
    }
    private IPoolable ReuseObject(Vector3 position, Quaternion rotation)
    {
        IPoolable pooledObject = objectPool.Primero();
        objectPool.Desacolar();
        pooledObject.GameObject.transform.position = position;
        pooledObject.GameObject.transform.rotation = rotation;
        pooledObject.GameObject.SetActive(true);

        return pooledObject;
    }
}
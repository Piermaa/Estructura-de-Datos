using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //--Esto se pone en cualquier objeto que cree un pool de objetos IPoolable -> Ej el arma del jugador que crea balas
    //--Poner el objeto vacio que contenga los objetos pooleados dentro de la var poolFolder serializada en el inspector.

    //Despues esto hay que portearlo a la cola que hicimos nosotros. Ahora no porque son las 4am y me da paja.
    //Cuando un objeto pooleado se muere que se desactive por su cuenta.

    //------PUBLIC PROPERTIES-------
    public bool IsPoolInited
    {
        get { return isPoolInited; }
    }

    //------PRIVATE PROPERTIES-------
    private bool isPoolInited = false;

    private Queue<IPoolable> objectPool;
    private IPoolable objectToPool;
    private int poolSize = 10;

    [SerializeField] private Transform poolFolder;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Awake()
    {
        objectPool = new Queue<IPoolable>();

        if (poolFolder == null)
        {
            Debug.LogWarning("Recordatorio: Objects in pool " + this.name + " don't have a parent.");
        }
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    public void EmptyPool()
    {
        foreach (IPoolable p_obj in objectPool)
        {
            Destroy(p_obj.GameObject);
        }
        objectPool.Clear();
    }

    public void CreatePool(IPoolable objectToPool, int poolMaxSize = 10)
    {
        if (objectPool != null)
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolMaxSize;
            isPoolInited = true;
        }
        else Debug.LogWarning("Object is not a poolable object.");
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

        objectPool.Enqueue(pooledObject);
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
        IPoolable pooledObject = objectPool.Dequeue();
        pooledObject.GameObject.transform.position = position;
        pooledObject.GameObject.transform.rotation = rotation;
        pooledObject.GameObject.SetActive(true);

        return pooledObject;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //--Esto se pone en cualquier objeto que cree un pool de objetos IPoolable -> Ej el arma del jugador que crea balas
    //--Poner el objeto vacio que contenga los objetos pooleados dentro de la var poolFolder serializada en el inspector.

    private bool isPoolInited = false;

    //Despues esto hay que portearlo a la cola que hicimos nosotros. Ahora no porque son las 4am y me da paja.
    //Cuando un objeto pooleado se muere que se desactive por su cuenta.

    private Queue<GameObject> objectPool;
    private GameObject objectToPool;
    private int poolSize = 10;

    //Asi las cosas pooleadas no toman el transform de su parent y se mueven con el porque eso en las balas no es bueno, no señor.
    //La otra es crear el objeto/carpeta que las contenga en runtime pero paja
    [SerializeField] private Transform poolFolder;

    //------PUBLIC PROPERTIES-------
    public bool IsPoolInited
    {
        get { return isPoolInited; }
    }


    //-----UNITY FUNCTIONS--------
    private void Awake()
    {
        objectPool = new Queue<GameObject>();

        if (poolFolder == null)
        {
            Debug.LogWarning("Aviso que los objetos de este pool " + this.name + " no tienen padre. Saludos.");
        }
    }

    //-----CLASS METHODS---------
    public void InitPool(GameObject objectToPool, int poolMaxSize = 10)
    {
        if (objectToPool.TryGetComponent(out IPoolable poolableObj))
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolMaxSize;
            isPoolInited = true;
        }
        else Debug.LogWarning("Alguien esta queriendo poolear un objeto no pooleable.");
    }

    public GameObject TryGetPooledObject()
    {
        GameObject pooledObject = null;

        if (objectPool.Count < poolSize)
        {
            pooledObject = NewObject();
        }
        else
        {
            pooledObject = ReuseObject();
        }

        objectPool.Enqueue(pooledObject);
        return pooledObject;
    }

    private GameObject NewObject()
    {
        GameObject pooledObject = Instantiate(objectToPool, transform.position, transform.rotation);
        pooledObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
        pooledObject.transform.SetParent(poolFolder);

        return pooledObject;
    }
    private GameObject ReuseObject()
    {
        GameObject pooledObject = objectPool.Dequeue();
        pooledObject.transform.position = transform.position;
        pooledObject.transform.rotation = transform.rotation;
        pooledObject.SetActive(true);

        return pooledObject;
    }
}

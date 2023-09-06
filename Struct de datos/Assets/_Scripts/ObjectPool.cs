using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //Despues esto hay que portearlo a la cola que hicimos nosotros. Ahora no porque son las 4am y me da paja.
    //Cuando un objeto pooleado se muere que se desactive por su cuenta.

    private Queue<GameObject> objectPool;
    private GameObject objectToPool;
    private int poolSize = 10;

    //Asi las cosas pooleadas no toman el transform de su parent y se mueven con el porque eso en las balas no es bueno, no señor.
    [SerializeField] private Transform poolFolder;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();

        if (poolFolder == null)
        {
            Debug.LogWarning("Aviso que los objetos de este pool " + this.name + " no tienen padre. Saludos.");
        }
    }

    public void InitPool(GameObject objectToPool, int poolMaxSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolMaxSize;
    }

    public GameObject GetPooledObject()
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

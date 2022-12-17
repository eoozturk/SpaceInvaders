using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Queue<GameObject> pooledObjects;

    public int poolSize;
    [SerializeField] private GameObject objPrefab;

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();
        for(int i=0; i<poolSize; i++)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);
            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject obj = pooledObjects.Dequeue();
        obj.SetActive(true);
        pooledObjects.Enqueue(obj);

        return obj;
    }
}

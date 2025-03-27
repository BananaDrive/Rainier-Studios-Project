using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public GameObject objectPoolHolder;
    
    [Serializable]
    public class ObjectsToPool
    {
        public GameObject pooledObj;
        public int poolSize;
    }
    public ObjectsToPool[] objectsToPool;
    public List<GameObject>[] listOfPooledObjects;

    void Awake()
    {
        if (SharedInstance != null && SharedInstance != this)
            Destroy(this);
        else
            SharedInstance = this;   

        InitializePool();
    }

    public void InitializePool()
    {
        listOfPooledObjects = new List<GameObject>[objectsToPool.Length];

        for (int i = 0; i < listOfPooledObjects.Length; i++)
        {
            Transform temp = Instantiate(objectPoolHolder).transform;
            temp.SetParent(transform);
            MakeObjectPool(i, temp);
        }
    }

    public void MakeObjectPool(int _i, Transform objectPoolTransform)
    {
        GameObject temp;
        objectPoolTransform.name = objectsToPool[_i].pooledObj.name + " pool " + _i;
        listOfPooledObjects[_i] = new List<GameObject>();

        for (int i = 0; i < objectsToPool[_i].poolSize; i++)
        {
            temp = Instantiate(objectsToPool[_i].pooledObj);
            temp.transform.SetParent(objectPoolTransform);
            temp.SetActive(false);
            listOfPooledObjects[_i].Add(temp);
        }
    }

    public GameObject GetPooledObject(int number)
    {
        for (int i = 0; i < objectsToPool[number].poolSize; i++)
        {
            if (!listOfPooledObjects[number][i].activeInHierarchy)
                return listOfPooledObjects[number][i];
        }
        return null;
    }

    public int GetObjectPoolNum(GameObject gameObject)
    {
        for (int i = 0; i < listOfPooledObjects.Length; i++)
        {
            if (gameObject.name + "(Clone)" == listOfPooledObjects[i][0].name)
                return i;
        }
        Debug.Log("Object not found");
        return 0;
    }
}
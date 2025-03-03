using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public GameObject objectPoolHolder;
    public List<GameObject>[] listOfPooledObjects;
    public GameObject[] objectsToPool;
    public int poolAmount;

    void Awake()
    {
        if (SharedInstance != null && SharedInstance != this)
            Destroy(this);
        else
            SharedInstance = this;   
    }

    void Start()
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
        objectPoolHolder.name = objectsToPool[_i].name + " pool " + _i;
        listOfPooledObjects[_i] = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < poolAmount; i++)
        {
            temp = Instantiate(objectsToPool[_i]);
            temp.transform.SetParent(objectPoolTransform);
            temp.SetActive(false);
            listOfPooledObjects[_i].Add(temp);
        }
    }

    public GameObject GetPooledObject(int number)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (!listOfPooledObjects[number][i].activeInHierarchy)
                return listOfPooledObjects[number][i];
        }
        return null;
    }
}

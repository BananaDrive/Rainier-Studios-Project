using System;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [Serializable]
    public class LootTables
    {
        public List<LootTableObj> lootTableObjects;
        public float tableMaxWeight;
        public string tableName;
        public void CalculateTotalWeight()
        {
            float total = 0;
            foreach (LootTableObj item in lootTableObjects)
            {
                total += item.objWeight;
                item.totalWeight = total;
            }
        }

    }

    [Serializable]
    public class LootTableObj
    {
        public GameObject tableObj;
        public float objWeight;
        internal float totalWeight;
    }

    public List<LootTables> lootTables;

    public void Start()
    {
        foreach (LootTables tables in lootTables)
        {
            tables.CalculateTotalWeight();
        }   
    }

    public GameObject FindLootTable(string _tableName)
    {
        for (int i = 0; i < lootTables.Count; i++)
        {
            if (lootTables[i].tableName == _tableName)
            {
                return DecideLoot(lootTables[i]);
            }
        }
        Debug.Log("Could not find loot table!");
        return null;
    }

    public GameObject DecideLoot(LootTables _lootTables)
    {
        float lootFloat = UnityEngine.Random.Range(0, _lootTables.tableMaxWeight);
        int left = 0;
        int right = _lootTables.lootTableObjects.Count - 1;

        while (left <= right)
        {
            int mid = Mathf.RoundToInt(left + (right - left) / 2);

            float _lootFloat = _lootTables.lootTableObjects[mid].totalWeight - lootFloat;

            if (_lootFloat >= 0 && _lootFloat < _lootTables.lootTableObjects[mid].objWeight)
                return _lootTables.lootTableObjects[mid].tableObj;
            if (_lootFloat < 0)
                left = mid + 1;
            else 
                right = mid - 1;
        }
        return null;
    }
}

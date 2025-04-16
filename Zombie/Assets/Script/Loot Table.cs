using System;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [Serializable]
    public class LootTables
    {
        public List<LootTableObj> lootTableObjects;
        public float tableTotalWeight;
        public string tableName;

    }
    [Serializable]
    public class LootTableObj
    {
        public GameObject tableObj;
        public float objWeight;
    }

    public List<LootTables> lootTables;

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
        float lootFloat = UnityEngine.Random.Range(0, _lootTables.tableTotalWeight);
        for (int i = 0; i < _lootTables.lootTableObjects.Count; i++)
        {
            lootFloat -= _lootTables.lootTableObjects[i].objWeight;
            if (lootFloat <= 0)
                return _lootTables.lootTableObjects[i].tableObj;
        }
        return null;
    }
}

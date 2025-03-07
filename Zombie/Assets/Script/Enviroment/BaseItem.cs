using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public SpriteRenderer sprite;
    internal BuffsHandler buffs;
    public enum ItemType
    {
        regen,
        damage,
        fireRate,
        speed,
        health,
        accuracy
    }

    [Serializable]
    public class ItemStats
    {
        public ItemType itemType;
        public float potency;
        public float duration;
    }
    public ItemStats[] itemsStats;

    public void UseItem()
    {
        for (int i = 0; i < itemsStats.Length; i++)
        {    
            switch (itemsStats[i].itemType)
            {
                case ItemType.regen:
                    CoroutineHandler.Instance.StartCoroutine(buffs.GetComponent<Health>().RegenerateHealth(itemsStats[i].potency, itemsStats[i].duration));
                break;
                case ItemType.damage:
                    CoroutineHandler.Instance.StartCoroutine(BuffDuration(buffs, nameof(BuffsHandler.damageBuff), i));
                break;
                case ItemType.fireRate:
                    CoroutineHandler.Instance.StartCoroutine(BuffDuration(buffs, nameof(BuffsHandler.fireRateBuff), i));
                break;
                case ItemType.speed:
                    CoroutineHandler.Instance.StartCoroutine(BuffDuration(buffs.GetComponent<Movement>(), nameof(Movement.moveSpeedBuff), i));
                break;
                case ItemType.health:
                    buffs.GetComponent<Health>().currentHealth += itemsStats[i].potency;
                break;
                case ItemType.accuracy:
                break;
            }
        }  
    }

    public IEnumerator BuffDuration<T>(T script, string buffName, int i)
    {
        FieldInfo field = script.GetType().GetField(buffName);
        field.SetValue(script, itemsStats[i].potency);
        yield return new WaitForSeconds(itemsStats[i].duration);
        field.SetValue(script, 0);
    }
}

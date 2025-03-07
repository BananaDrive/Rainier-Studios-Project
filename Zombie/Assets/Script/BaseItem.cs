using System;
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
            string temp;
            switch (itemsStats[i].itemType)
            {
                case ItemType.regen:
                    CoroutineHandler.Instance.StartCoroutine(buffs.GetComponent<Health>().RegenerateHealth(itemsStats[i].potency, itemsStats[i].duration));
                break;
                case ItemType.damage:
                    temp = nameof(BuffsHandler.damageBuff);
                    buffs.StoreCouroutine(temp, CoroutineHandler.Instance.StartCoroutine(buffs.BuffDuration(temp, itemsStats[i])));
                break;
                case ItemType.fireRate:
                    temp = nameof(BuffsHandler.fireRateBuff);
                    buffs.StoreCouroutine(temp, CoroutineHandler.Instance.StartCoroutine(buffs.BuffDuration(temp, itemsStats[i])));
                break;
                case ItemType.speed:
                    temp = nameof(BuffsHandler.moveSpeedBuff);
                    CoroutineHandler.Instance.StartCoroutine(buffs.BuffDuration(temp, itemsStats[i]));
                break;
                case ItemType.health:
                    buffs.GetComponent<Health>().currentHealth += itemsStats[i].potency;
                break;
                case ItemType.accuracy:
                break;
            }
        }  
    }

    
}

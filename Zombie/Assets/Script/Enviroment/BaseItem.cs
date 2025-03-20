using System;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public SpriteRenderer sprite;
    internal BuffsHandler buffs;
    public enum ItemType
    {
        health,
        regen,
        damage,
        fireRate,
        speed,
        accuracy,
        placeable

    }

    [Serializable]
    public class ItemStats
    {
        public ItemType itemType;
        public float potency;
        public float duration;
    }
    public ItemStats[] itemsStats;
    public bool canPickUp = true;

    public void UseItem()
    {
        for (int i = 0; i < itemsStats.Length; i++)
        {    
            string temp = "";
            switch (itemsStats[i].itemType)
            {
                case ItemType.health:
                    buffs.GetComponent<Health>().currentHealth += itemsStats[i].potency;
                break;
                case ItemType.regen:
                    CoroutineHandler.Instance.StartCoroutine(buffs.GetComponent<Health>().RegenerateHealth(itemsStats[i].potency, itemsStats[i].duration));
                break;
                case ItemType.damage:
                    temp = nameof(BuffsHandler.damageBuff);
                break;
                case ItemType.fireRate:
                    temp = nameof(BuffsHandler.fireRateBuff);
                break;
                case ItemType.speed:
                    temp = nameof(BuffsHandler.moveSpeedBuff);
                break;
                case ItemType.accuracy:
                    temp = nameof(BuffsHandler.accuracyBuff);
                break;
                case ItemType.placeable:
                    GetComponent<Traps>().layerToAvoid = 9;
                    canPickUp = false;
                    transform.position = new Vector2(buffs.transform.position.x, buffs.transform.position.y - 0.35f);
                    gameObject.SetActive(true);
                break;
            }
            
            if (temp != "")
                buffs.StoreCouroutine(temp, buffs.StartCoroutine(buffs.BuffDuration(temp, itemsStats[i])));
        }  
    }
}
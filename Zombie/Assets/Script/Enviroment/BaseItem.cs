using System;
using UnityEngine;

    public enum ItemType
    {
        health,
        regen,
        damage,
        fireRate,
        speed,
        accuracy,
        bulletSpeed,
        placeable
    }

    [Serializable]
    public class ItemStats
    {
        public ItemType itemType;
        public float potency;
        public float duration;
        public bool isStackable;
    }

public class BaseItem : MonoBehaviour
{
    public SpriteRenderer sprite;
    internal BuffsHandler buffs;

    public ItemStats[] itemsStats;
    public bool canPickUp = true;

    public void UseItem()
    {
        foreach (ItemStats items in itemsStats)
        {    
            switch (items.itemType)
            {
                case ItemType.health:
                    buffs.GetComponent<Health>().currentHealth += items.potency;
                break;
                case ItemType.regen:
                    CoroutineHandler.Instance.StartCoroutine(buffs.GetComponent<Health>().RegenerateHealth(items.potency, items.duration));
                break;
                case ItemType.placeable:
                    GetComponent<Traps>().layerToAvoid = 1 << buffs.gameObject.layer;
                    transform.position = new Vector2(buffs.transform.position.x, buffs.transform.position.y - 0.35f);
                    gameObject.SetActive(true);
                break;
                default:
                    buffs.AddBuff(items);
                break;
            }
            canPickUp = false;
        }  
    }
}
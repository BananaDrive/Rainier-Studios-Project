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
        damageReduc,
        placeable
    }

    [Serializable]
    public class ItemStats
    {
        public Sprite sprite;
        public ItemType itemType;
        public float potency;
        public float duration;
        public bool isStackable;
    }

public class BaseItem : MonoBehaviour
{
    public Sprite sprite;
    internal BuffsHandler buffs;

    public ItemStats[] itemsStats;
    public bool canPickUp = true;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void UseItem()
    {
        foreach (ItemStats items in itemsStats)
        {    
            switch (items.itemType)
            {
                case ItemType.health:
                    buffs.GetComponent<Health>().currentHealth += items.potency;
                break;
                case ItemType.placeable:
                    Placeable placeable = GetComponent<Placeable>();
                    placeable.layerToAvoid = 1 << buffs.gameObject.layer;
                    transform.SetPositionAndRotation(new Vector2(buffs.transform.position.x, buffs.transform.position.y - 0.35f), Quaternion.Euler(0, buffs.transform.eulerAngles.y, 0));
                    EnableScripts();
                break;
                default:
                    buffs.AddBuff(items);
                break;
            }
            canPickUp = false;
        }  
    }

    public void EnableScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour monoBehaviour in scripts)
            monoBehaviour.enabled = true;
        gameObject.SetActive(true);
    }
}
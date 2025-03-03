using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public enum ItemType
    {
        regen,
        damage,
        fireRate,
        speed,
        health,
        accuracy
    }

    public SpriteRenderer sprite;
    internal GameObject player;
    public ItemType itemType;
    public float potency;
    public float duration;
    public string itemName;




    public void UseItem()
    {
        switch (itemType)
        {
            case ItemType.regen:
                player.GetComponent<Health>().currentHealth += potency;
            break;
            case ItemType.damage:
                StartCoroutine(BuffDuration(player.GetComponent<Weapon>(), nameof(Weapon.damageBuff)));
            break;
            case ItemType.fireRate:
            break;
            case ItemType.speed:
            break;
            case ItemType.health:
            break;
            case ItemType.accuracy:
            break;
        }

        
    }

    public IEnumerator BuffDuration<T>(T script, string name)
    {
        FieldInfo field = script.GetType().GetField(name, BindingFlags.Public);
        field.SetValue(script, potency);
        yield return new WaitForSeconds(duration);
        field.SetValue(script, 0);
    }
}

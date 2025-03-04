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
            break;
            case ItemType.damage:
                CoroutineHandler.Instance.StartCoroutine(BuffDuration(player.GetComponent<Inventory>(), nameof(Inventory.damageBuff)));
            break;
            case ItemType.fireRate:
                CoroutineHandler.Instance.StartCoroutine(BuffDuration(player.GetComponent<Inventory>(), nameof(Inventory.fireRateBuff)));
            break;
            case ItemType.speed:
                CoroutineHandler.Instance.StartCoroutine(BuffDuration(player.GetComponent<Movement>(), nameof(Movement.moveSpeedBuff)));
            break;
            case ItemType.health:
                player.GetComponent<Health>().currentHealth += potency;
            break;
            case ItemType.accuracy:
            break;
        }

        
    }

    public IEnumerator BuffDuration<T>(T script, string buffName)
    {
        FieldInfo field = script.GetType().GetField(buffName);
        field.SetValue(script, potency);
        yield return new WaitForSeconds(duration);
        field.SetValue(script, 0);
    }
}

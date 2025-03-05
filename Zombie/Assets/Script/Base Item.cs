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
    internal BuffsHandler buffs;
    public ItemType[] itemTypes;
    public float[] potency;
    public float duration;
    public string itemName;


    public void UseItem()
    {
        for (int i = 0; i < itemTypes.Length; i++)
        {    
            switch (itemTypes[i])
            {
                case ItemType.regen:
                    CoroutineHandler.Instance.StartCoroutine(buffs.GetComponent<Health>().RegenerateHealth(potency[i], duration));
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
                    buffs.GetComponent<Health>().currentHealth += potency[i];
                break;
                case ItemType.accuracy:
                break;
            }
        }  
    }

    public IEnumerator BuffDuration<T>(T script, string buffName, int potencyCount)
    {
        FieldInfo field = script.GetType().GetField(buffName);
        field.SetValue(script, potency[potencyCount]);
        yield return new WaitForSeconds(duration);
        field.SetValue(script, 0);
    }
}

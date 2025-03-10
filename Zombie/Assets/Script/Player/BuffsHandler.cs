using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    public Dictionary<string, Coroutine> buffDict = new();
    
    [Header("Buffs")]
    public float damageBuff;
    public float fireRateBuff, clipSizeBuff, bulletSpeedBuff, moveSpeedBuff;


    public IEnumerator BuffDuration(string buffName, BaseItem.ItemStats stats)
    {
        FieldInfo field = GetType().GetField(buffName);
        field.SetValue(this, stats.potency);
        yield return new WaitForSeconds(stats.duration);
        buffDict[buffName] = null;
        field.SetValue(this, 0);
    }

    public void StoreCouroutine(string buffName, Coroutine coroutine)
    {
        if (!buffDict.ContainsKey(buffName))
        {
            buffDict.Add(buffName, coroutine);
            return;
        }
        if (buffDict[buffName] != null)
        {
            StopCoroutine(buffDict[buffName]);
            Debug.Log("SToppd");
            buffDict[buffName] = null;
        }
        buffDict[buffName] = coroutine;
    }
}

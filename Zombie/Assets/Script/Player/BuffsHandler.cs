using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    public LayerMask player;
    public Dictionary<string, Coroutine> buffDict = new();
    
    [Header("Buffs")]
    public float damageBuff;
    public float fireRateBuff, clipSizeBuff, bulletSpeedBuff, moveSpeedBuff, accuracyBuff;

    [Header("Enhancers")]
    public float damageEnhance;
    public float fireRateEnhance, clipSizeEnhance, bulletSpeedEnhance, accuracyEnhance, reloadEnhance, shotAmountEnhance;

    public bool allowRaycast;
    public bool allowPiercing;
    public bool allowAuto;

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
            buffDict[buffName] = null;
        }
        buffDict[buffName] = coroutine;
    }
}

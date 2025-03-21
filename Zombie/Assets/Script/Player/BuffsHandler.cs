using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    public Movement movement;
    public Weapon weapon;

    public List<ItemStats> buffList = new();
    
    [Header("Buffs")]
    public float damageBuff;
    public float fireRateBuff, clipSizeBuff, bulletSpeedBuff, moveSpeedBuff, accuracyBuff;

    [Header("Enhancers")]
    public float damageEnhance;
    public float fireRateEnhance, clipSizeEnhance, bulletSpeedEnhance, accuracyEnhance, reloadEnhance, shotAmountEnhance;

    public bool allowRaycast;
    public bool allowPiercing;
    public bool allowAuto;



    public void FixedUpdate()
    {
        foreach (ItemStats itemStats in buffList)
        {
            itemStats.duration -= Time.deltaTime;

            if (itemStats.duration <= 0f)
                buffList.Remove(itemStats);
        }
        ApplyBuffs();
    }

    public void AddBuff(ItemStats newBuff)
    {
        if (!newBuff.isStackable)
        {
            buffList.RemoveAll(ItemStats => ItemStats.itemType == newBuff.itemType);
        }
        buffList.Add(newBuff);
        ApplyBuffs();
    }

    public void ApplyBuffs()
    {
        damageBuff = 0;
        fireRateBuff = 0;
        moveSpeedBuff = 0;
        accuracyBuff = 0;
        bulletSpeedBuff = 0;
        
        foreach (ItemStats activeBuffs in buffList)
        {
            switch (activeBuffs.itemType)
            {
                case ItemType.damage:
                    damageBuff += activeBuffs.potency / 100;
                break;
                case ItemType.fireRate:
                    fireRateBuff += activeBuffs.potency / 100;
                break;
                case ItemType.speed:
                    moveSpeedBuff += activeBuffs.potency / 100;
                break;
                case ItemType.accuracy:
                    accuracyBuff += activeBuffs.potency / 100;
                break;
                case ItemType.bulletSpeed:
                    bulletSpeedBuff += activeBuffs.potency / 100;
                break;
            }
        }
        UpdateStats();
    }

    public void ApplyEnhancer(Enhancers enhancer)
    {
        damageEnhance += enhancer.damage;
        fireRateEnhance += enhancer.fireRate;
        clipSizeEnhance += enhancer.clipSize;
        bulletSpeedEnhance += enhancer.bulletSpeed;
        reloadEnhance += enhancer.reloadSpeed;
        accuracyEnhance += enhancer.accuracy;
        shotAmountEnhance += enhancer.shotAmount;

        bulletSpeedEnhance = Mathf.Clamp(bulletSpeedEnhance, -80, 1000);

        allowAuto = !enhancer.disableAuto && (enhancer.allowAuto || allowAuto);
        allowPiercing = !enhancer.disablePiercing && (enhancer.allowPiercing || allowPiercing);
        allowRaycast = !enhancer.disableRaycast && (enhancer.allowRaycast || allowRaycast);
    }

    public void UpdateStats()
    {
        weapon.damageBuff = 1 + damageEnhance / 100 * damageBuff;
        weapon.fireRateBuff = 1 + fireRateEnhance / 100 * fireRateBuff;
        movement.moveSpeedBuff = 1 + moveSpeedBuff;
        weapon.accuracyBuff = 1 + accuracyEnhance / 100 * accuracyBuff;
        weapon.bulletSpeedBuff = 1 + bulletSpeedEnhance / 100 * bulletSpeedBuff;

        weapon.allowAuto = allowAuto;
        weapon.allowRaycast = allowRaycast;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    public Movement movement;
    public Weapon weapon;
    public PlayerHealth health;

    public List<ItemStats> buffList = new();
    
    [Header("Buffs")]
    public float damageBuff;
    public float fireRateBuff, clipSizeBuff, bulletSpeedBuff, moveSpeedBuff, accuracyBuff, regenBuff, damageReducBuff;

    [Header("Enhancers")]
    public float damageEnhance;
    public float fireRateEnhance, clipSizeEnhance, bulletSpeedEnhance, accuracyEnhance, reloadEnhance, shotAmountEnhance;

    public bool allowRaycast;
    public bool allowPiercing;
    public bool allowAuto;



    public void FixedUpdate()
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].duration -= Time.deltaTime;

            if (buffList[i].duration <= 0f)
                buffList.Remove(buffList[i]);
        }
        GameManager.Instance.UIManager.DisplayBuffs(buffList);
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
        damageBuff = 100;
        fireRateBuff = 100;
        moveSpeedBuff = 100;
        accuracyBuff = 100;
        bulletSpeedBuff = 100;
        damageReducBuff = 100;
        regenBuff = 0;
        
        foreach (ItemStats activeBuffs in buffList)
        {
            switch (activeBuffs.itemType)
            {
                case ItemType.damage:
                    damageBuff += activeBuffs.potency;
                break;
                case ItemType.fireRate:
                    fireRateBuff += activeBuffs.potency;
                break;
                case ItemType.speed:
                    moveSpeedBuff += activeBuffs.potency;
                break;
                case ItemType.accuracy:
                    accuracyBuff += activeBuffs.potency;
                break;
                case ItemType.bulletSpeed:
                    bulletSpeedBuff += activeBuffs.potency;
                break;
                case ItemType.regen:
                    regenBuff += activeBuffs.potency;
                break;
                case ItemType.damageReduc:
                    damageReducBuff += activeBuffs.potency;
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
        weapon.damageBuff = (damageEnhance + damageBuff) / 100;
        weapon.fireRateBuff = (fireRateEnhance + fireRateBuff) / 100;
        weapon.accuracyBuff = (accuracyEnhance + accuracyBuff) / 100;
        weapon.bulletSpeedBuff = (bulletSpeedEnhance + bulletSpeedBuff) / 100;
        weapon.allowAuto = allowAuto;
        weapon.allowRaycast = allowRaycast;
        weapon.allowPiercing = allowPiercing;

        movement.moveSpeedBuff = moveSpeedBuff / 100;

        health.regenAmount = regenBuff;
        health.damageReduc = damageReducBuff;
    }
}

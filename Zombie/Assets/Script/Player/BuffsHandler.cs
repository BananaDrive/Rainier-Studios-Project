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


    void Awake()
    {
        if (GameManager.Instance.buffsHandler == null)
        {
            GameManager.Instance.buffsHandler = this;
        }
        else if (GameManager.Instance.buffsHandler != this)
        {
            CopyStats(GameManager.Instance.buffsHandler);
            Destroy(GameManager.Instance.buffsHandler.gameObject);
            GameManager.Instance.buffsHandler = this;
            Debug.Log("works");
        }

        DontDestroyOnLoad(GameManager.Instance.buffsHandler);
    }

    public void FixedUpdate()
    {
        Debug.Log(GameManager.Instance.buffsHandler.fireRateEnhance);
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
        damageBuff = 0;
        fireRateBuff = 0;
        moveSpeedBuff = 0;
        accuracyBuff = 0;
        bulletSpeedBuff = 0;
        damageReducBuff = 0;
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
        weapon.damageBuff = NormalizeBuff(damageEnhance + damageBuff);
        weapon.fireRateBuff = NormalizeBuff(fireRateEnhance + fireRateBuff);
        weapon.accuracyBuff = NormalizeBuff(accuracyEnhance + accuracyBuff);
        weapon.bulletSpeedBuff = NormalizeBuff(bulletSpeedEnhance + bulletSpeedBuff);

        weapon.allowAuto = allowAuto;
        weapon.allowRaycast = allowRaycast;
        weapon.allowPiercing = allowPiercing;

        movement.moveSpeedBuff = NormalizeBuff(moveSpeedBuff);

        health.regenAmount = regenBuff;
        health.damageReduc = NormalizeBuff(damageReducBuff);
    }

    public void CopyStats(BuffsHandler buffs)
    {
        damageEnhance = buffs.damageEnhance;
        fireRateEnhance = buffs.fireRateEnhance;
        clipSizeEnhance = buffs.clipSizeEnhance;
        bulletSpeedEnhance = buffs.bulletSpeedEnhance;
        reloadEnhance = buffs.reloadEnhance;
        accuracyEnhance = buffs.accuracyEnhance;
        shotAmountEnhance = buffs.shotAmountEnhance;

        allowAuto = buffs.allowAuto;
        allowPiercing = buffs.allowPiercing;
        allowRaycast = buffs.allowRaycast;
    }

    public float NormalizeBuff(float totalBuff)
    {
        if (totalBuff <= 0)
            return 100f / (Mathf.Abs(totalBuff) + 100f);
        else 
            return totalBuff / 100f;
    }
}

using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    [Header("Buffs")]
    public float damageBuff;
    public float fireRateBuff, clipSizeBuff, bulletSpeedBuff;

    [Header("Enhancers")]
    public float damageEnhance;
    public float fireRateEnhance, clipSizeEnhance, bulletSpeedEnhance, accuracyEnhance, reloadEnhance, shotAmountEnhance;

    public bool allowRaycast;
    public bool allowPiercing;
    public bool allowAuto;
}

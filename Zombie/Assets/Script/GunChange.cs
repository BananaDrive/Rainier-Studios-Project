using UnityEngine;

public class GunChange : MonoBehaviour
{
    public Weapon weapon;
    public BuffsHandler buffs;

    public class Weapon
    {
        public float damage, fireRate, clipSize, bulletSpeed, accuracy, reloadSpeed, shotAmount;
    }

    [Header("Stat Requirements")]
    public float damage;
    public float fireRate, clipSize, bulletSpeed, accuracy, reloadSpeed, shotAmount;

    [Header("Stat Milestones")]
    public bool damageHit;
    public bool fireRateHit, clipSizeHit, bulletSpeedHit, accuracyHit, reloadSpeedHit, shotAmountHit;
    void Update()
    {
        
    }

    public void DecideRatios()
    {
        damageHit = buffs.damageEnhance >= damage;
        fireRateHit = buffs.fireRateEnhance >= fireRate;
        clipSizeHit = buffs.clipSizeEnhance >= clipSize;
        bulletSpeedHit = buffs.bulletSpeedEnhance >= bulletSpeed;
        accuracyHit = buffs.accuracyEnhance >= accuracy;
        reloadSpeedHit = buffs.reloadEnhance >= reloadSpeed;
        shotAmountHit = buffs.shotAmountEnhance >= shotAmount;
    }
}

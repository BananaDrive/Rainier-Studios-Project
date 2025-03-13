using TMPro;
using UnityEngine;

public class Enhancers : MonoBehaviour
{
    public string itemName;
    public string itemStats;
    [Header("Stats")]
    public float damage;
    public float fireRate, accuracy, reloadSpeed, bulletSpeed, shotAmount, clipSize;

    [Header("Upgrades")]
    public bool allowRaycast;
    public bool allowPiercing, allowAuto;
    public bool disableRaycast, disablePiercing, disableAuto;
}

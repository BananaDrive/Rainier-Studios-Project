using TMPro;
using UnityEngine;

public class Enhancers : MonoBehaviour
{
    public TMP_Text itemText, itemStatText;
    public GameObject itemPanel;
    public string itemName;
    public string itemStats;
    [Header("Stats")]
    public float damage;
    public float fireRate, accuracy, reloadSpeed, bulletSpeed, shotAmount, clipSize;

    [Header("Upgrades")]
    public bool allowRaycast;
    public bool allowPiercing, allowAuto;
    public bool disableRaycast, disablePiercing, disableAuto;


    public void TogglePanel()
    {
        itemPanel.SetActive(!itemPanel.activeSelf);
        itemText.SetText(itemName);
        itemStatText.SetText(itemStats);
    }
    
}

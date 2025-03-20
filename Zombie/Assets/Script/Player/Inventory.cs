using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject weapon;
    public BuffsHandler buffs;
    internal BaseItem foundItem;
    internal Enhancers enhancer;
    public LayerMask itemLayer, enhancerLayer;
    public BaseItem[] Items;


    public void Start()
    {
        Items = new BaseItem[4];
    }

    public void FixedUpdate()
    {
        ItemDetection();
        EnhancerDetection();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && enhancer != null)
        {
            ApplyEnhancer();
            enhancer.gameObject.SetActive(false);
            enhancer = null;
            return;
        }
        if (Input.GetKeyDown(KeyCode.F) && Items[0] != null && enhancer == null)
        {
            Items[0].UseItem();
            Items[0] = null;
            SortInv();
        }
    }

    public void ItemDetection()
    {
        foundItem = null;
        float minDistance = 2f;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, itemLayer))
        {
            float itemDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (itemDistance < minDistance)
            {
                foundItem = collider.GetComponent<BaseItem>();
                minDistance = itemDistance;
            }
        }

        int temp = CheckInventory();

        if (foundItem != null && foundItem.canPickUp && temp != 4)
        {
            foundItem.buffs = buffs;
            Items[temp] = foundItem;
            foundItem.gameObject.SetActive(false);
        }
    }

    public void EnhancerDetection()
    {
        if (enhancer == null)
            GameManager.Instance.UIManager.itemStatsPanel.SetActive(false);

        enhancer = null;
        float minDistance = 2f;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, enhancerLayer))
        {
            float itemDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (itemDistance < minDistance)
            {
                enhancer = collider.GetComponent<Enhancers>();
                minDistance = itemDistance;
            }
        }

        if (enhancer != null)
            GameManager.Instance.UIManager.ChangeItemPanel(enhancer.itemName, enhancer.itemStats);
    }

    public void ApplyEnhancer()
    {
        buffs.damageEnhance += enhancer.damage;
        buffs.fireRateBuff += enhancer.fireRate;
        buffs.clipSizeEnhance += enhancer.clipSize;
        buffs.bulletSpeedEnhance += enhancer.bulletSpeed;
        buffs.reloadEnhance += enhancer.reloadSpeed;
        buffs.accuracyEnhance += enhancer.accuracy;
        buffs.shotAmountEnhance += enhancer.shotAmount;

        buffs.bulletSpeedEnhance = Mathf.Clamp(buffs.bulletSpeedEnhance, -80, 1000);

        buffs.allowAuto = !enhancer.disableAuto && (enhancer.allowAuto || buffs.allowAuto);
        buffs.allowPiercing = !enhancer.disablePiercing && (enhancer.allowPiercing || buffs.allowPiercing);
        buffs.allowRaycast = !enhancer.disableRaycast && (enhancer.allowRaycast || buffs.allowRaycast);
    }

    public int CheckInventory()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return i;
        }
        return 4;
    }

    public void SortInv()
    {
        for (int i = 0; i < Items.Length - 1; i++)
        {
            Items[i] = Items[i + 1];
            Items[i + 1] = null;
        }
    }
}

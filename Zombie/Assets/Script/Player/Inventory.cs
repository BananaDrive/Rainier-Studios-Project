using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject weapon;
    public BuffsHandler buffs;
    internal BaseItem foundItem;
    internal Enhancers enhancer;
    internal Gate gate;
    public LayerMask itemLayer, enhancerLayer, leverLayer;
    public BaseItem[] Items;


    public void Start()
    {
        Items = new BaseItem[4];
    }

    public void FixedUpdate()
    {
        ItemDetection();
        EnhancerDetection();
        GateDetection();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && gate != null && gate.canOpen)
        {
            gate.OpenGate();
            return;
        }
        if (Input.GetKeyDown(KeyCode.F) && enhancer != null)
        {
            buffs.ApplyEnhancer(enhancer);
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

    public void GateDetection()
    {
        gate = null;
        float minDistance = 2f;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, leverLayer))
        {
            float gateDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (gateDistance < minDistance)
            {
                gate = collider.GetComponent<Gate>();
                minDistance = gateDistance;
            }
        }
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
